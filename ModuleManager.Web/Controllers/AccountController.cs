using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using ModuleManager.Domain;
using ModuleManager.UserDAL;
using ModuleManager.UserDAL.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ModuleManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISysteemRolRepository _systeemRolRepository;

        public AccountController(ISysteemRolRepository systeemRolRepository)
        {
            _systeemRolRepository = systeemRolRepository;
        }

        private ServiceProviderDescription AvansServiceDescription
        {
            get
            {
                return new ServiceProviderDescription
                {
                    AccessTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/access_token", HttpDeliveryMethods.GetRequest),
                    RequestTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/request_token", HttpDeliveryMethods.GetRequest),
                    UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/saml.php", HttpDeliveryMethods.GetRequest),
                    TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
                    ProtocolVersion = ProtocolVersion.V10a
                };
            }
        }

        public class SessionStateConsumerTokenManager : IConsumerTokenManager
        {
            private static readonly string SESSION_KEY_TOKENS = typeof(SessionStateConsumerTokenManager).FullName + ".Tokens";

            /// <summary>
            /// Gets the consumer key used to sign OAuth requests and responses.
            /// </summary>
            public string ConsumerKey
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the consumer secret used to sign OAuth requests and responses.
            /// </summary>
            public string ConsumerSecret
            {
                get;
                private set;
            }

            public HttpSessionStateBase Session
            {
                get; set;
            }

            /// <summary>
            /// Initialises a new SessionStateConsumerTokenManager using the specified consumer key and secret.
            /// </summary>
            /// <param name="consumerKey">The consumer key to be used when signing OAuth requests.</param>
            /// <param name="consumerSecret">The consumer secret to be used when signing OAuth requests.</param>
            public SessionStateConsumerTokenManager(string consumerKey, string consumerSecret, HttpSessionStateBase session)
            {
                this.ConsumerKey = consumerKey;
                this.ConsumerSecret = consumerSecret;
                this.Session = session;
            }

            public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
            {
                var storage = this.GetTokenStorage();
                if (!storage.Remove(requestToken))
                {
                    throw new ArgumentException("Specified requestToken doesn't exist");
                }

                storage[accessToken] = new InMemoryToken() { Token = accessToken, TokenType = TokenType.AccessToken, TokenSecret = accessTokenSecret };
            }

            public string GetTokenSecret(string token)
            {
                return this.GetTokenMetadata(token).TokenSecret;
            }

            public TokenType GetTokenType(string token)
            {
                return this.GetTokenMetadata(token).TokenType;
            }

            private InMemoryToken GetTokenMetadata(string token)
            {
                var storage = this.GetTokenStorage();
                InMemoryToken inMemoryToken = null;
                if (!storage.TryGetValue(token, out inMemoryToken))
                {
                    throw new ArgumentException("Cannot find secret for specified token");
                }

                return inMemoryToken;
            }

            public void StoreNewRequestToken(DotNetOpenAuth.OAuth.Messages.UnauthorizedTokenRequest request, DotNetOpenAuth.OAuth.Messages.ITokenSecretContainingMessage response)
            {
                var storage = this.GetTokenStorage();
                InMemoryToken token = new InMemoryToken() { Token = response.Token, TokenSecret = response.TokenSecret, TokenType = TokenType.RequestToken };

                storage[token.Token] = token;
            }

            private IDictionary<string, InMemoryToken> GetTokenStorage()
            {
                AssertSessionState();

                IDictionary<string, InMemoryToken> storage = Session[SESSION_KEY_TOKENS] as IDictionary<string, InMemoryToken>;

                if (storage == null)
                {
                    storage = new Dictionary<string, InMemoryToken>();
                    Session[SESSION_KEY_TOKENS] = storage;
                }

                return storage;
            }

            private void AssertSessionState()
            {
                if (Session == null)
                {
                    throw new InvalidOperationException("Session state is unavailable");
                }
            }

            [Serializable]
            protected class InMemoryToken
            {
                public TokenType TokenType { get; set; }
                public string Token { get; set; }
                public string TokenSecret { get; set; }

                public InMemoryToken()
                {
                    this.TokenType = DotNetOpenAuth.OAuth.ChannelElements.TokenType.InvalidToken;
                }
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            var consumer = new WebConsumer(AvansServiceDescription, new SessionStateConsumerTokenManager(ConfigurationManager.AppSettings["avansConsumerKey"], ConfigurationManager.AppSettings["avansConsumerSecret"], Session));
            
            var accessToken = consumer.ProcessUserAuthorization();
            
            if(accessToken == null)
            {
                // Send to login page
                consumer.Channel.Send(consumer.PrepareRequestUserAuthorization());
            } else
            {
                var responseStream = consumer.PrepareAuthorizedRequestAndSend(new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/people/@me", HttpDeliveryMethods.GetRequest), accessToken.AccessToken).ResponseStream;
                var response = new StreamReader(responseStream).ReadToEnd();

                var avansDetails = JObject.Parse(response);

                var name = (string)avansDetails["nickname"];
                var isEmployee = (string)avansDetails["employee"] == "true";
                var email = (string)avansDetails["emails"][0];
                var username = (string)avansDetails["accounts"]["username"];



            


                if (!isEmployee)
                {
                    throw new Exception("Alleen docenten mogen inloggen!");
                }

                using (var context = new DomainEntities())
                {
                    var user = context.User.FirstOrDefault(u => u.UserNaam == username);

                    if(user == null)
                    {
                        //retrieve image
                        var responseStreamImage = consumer.PrepareAuthorizedRequestAndSend(new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/medewerkersgids/image/" + username, HttpDeliveryMethods.GetRequest), accessToken.AccessToken).ResponseStream;
                        var responseImage = new StreamReader(responseStreamImage).ReadToEnd();
                        var avansImage = JObject.Parse('{' + responseImage.Split('{')[1]);

                        // Create new user
                        user = context.User.Create();
                        user.UserNaam = username;
                        user.email = email;
                        user.naam = name;
                        user.Image = (string)avansImage["image"];
                        user.SysteemRol = "Docent";
                        context.User.Add(user);
                        context.SaveChanges();
                    }

                    // Save role of the user in ticket
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.UserNaam, DateTime.Now, DateTime.Now.AddYears(10), true, user.SysteemRol, "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
            }    
            return null;
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
using ModuleManager.UserDAL;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Web.WebPages;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        /**
         * Save tokens in the session
         */
        public class AvansTokenManager : IConsumerTokenManager
        {
            public string ConsumerKey
            {
                get
                {
                    return ConfigurationManager.AppSettings["avansConsumerKey"];
                }
            }

            public string ConsumerSecret
            {
                get
                {
                    return ConfigurationManager.AppSettings["avansConsumerSecret"];
                }
            }
            private HttpSessionStateBase Session;

            public AvansTokenManager(HttpSessionStateBase session)
            {
                this.Session = session;
            }

            public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
            {
                Session.Remove("oauth_" + requestToken);
                Session["oauth_" + accessToken] = accessTokenSecret;
            }

            public string GetTokenSecret(string token)
            {
                return Session["oauth_" + token] as string;
            }

            public TokenType GetTokenType(string token)
            {
                throw new NotImplementedException("should not be called");
            }

            public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
            {
                Session["oauth_" + response.Token] = response.TokenSecret;
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {   
            var consumer = new WebConsumer(AvansServiceDescription, new AvansTokenManager(Session));
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

                using (var context = new UserContext())
                {
                    var user = context.User.FirstOrDefault(u => u.UserNaam == username);

                    if(user == null)
                    {
                        // Create new user
                        user = context.User.Create();
                        user.UserNaam = username;
                        user.email = email;
                        user.Blocked = false;
                        user.naam = name;
                        user.SysteemRol = "";
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
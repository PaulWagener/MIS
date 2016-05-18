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
                var responseStream = consumer.PrepareAuthorizedRequestAndSend(new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/studentnummer/", HttpDeliveryMethods.GetRequest), accessToken.AccessToken).ResponseStream;
                var response = new StreamReader(responseStream).ReadToEnd();
                var lol = JsonValue.Parse("");
                Console.WriteLine(accessToken.AccessToken);
            }
            
            return null;
        }
        [HttpPost]
        public ActionResult LogIn(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var ReturnValue = AuthenticateUser(loginVM.UserNaam, loginVM.Wachtwoord);
                if (ReturnValue == 1)
                {
                    // Create the authentication cookie and redirect the user to welcome page
                    FormsAuthentication.RedirectFromLoginPage(loginVM.UserNaam, loginVM.Remember);
                    //FormsAuthentication.SetAuthCookie()
                    String role = GetRole(loginVM.UserNaam);
                    if (!role.IsEmpty())
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1, // Ticket version
                            loginVM.UserNaam, // Username associated with ticket
                            DateTime.Now, // Date/time issued
                            DateTime.Now.AddMinutes(30), // Date/time to expire
                            true, // "true" for a persistent user cookie
                            role, // User-data, in this case the roles
                            FormsAuthentication.FormsCookiePath);

                        // Encrypt the cookie using the machine key for secure transport
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(
                            FormsAuthentication.FormsCookieName, // Name of auth cookie
                            hash); // Hashed ticket
                        FormsAuthentication.SetAuthCookie(loginVM.UserNaam, loginVM.Remember);

                        // Set the cookie's expiration time to the tickets expiration time
                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }

                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (ReturnValue == -2)
                {
                    ViewBag.LoginError = "Deze gebruikersnaam is geblokeerd";
                }
                else if (ReturnValue == -1)
                {
                    ViewBag.LoginError = "Ongeldige gebruikersnaam en/of wachtwoord";
                }
                else
                {
                    ViewBag.LoginError = "Probeer het later opnieuw";
                }
            }
            return View(loginVM);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private int? AuthenticateUser(String username, String password)
        {

            using (var context = new UserContext())
            {

                String hashedPassword = GetSwcSH1(password);

                int? result;
                try
                {
                    var resultlist = context.spAuthenticateUser(username, hashedPassword).ToList();
                    result = resultlist.SingleOrDefault();
                }
                catch 
                {
                    result = -3;
                }

                return result;
            }

        }

        public String GetRole(String username)
        {
            using (var context = new UserContext())
            {

                String result;
                try
                {
                    var resultlist = context.spGetRol(username);
                    var list = new List<string>();

                    list = (from element in resultlist select element).ToList();
                    result = list.SingleOrDefault();
                }
                catch (Exception)
                {
                    result = String.Empty;
                }

                return result;
            }
        }

        public static string GetSwcSH1(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

    }
}
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ModuleManager.Web.App_Start;
using System;
using System.Web.Security;
using System.Security.Principal;

namespace ModuleManager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            /**
             * The line below exists to force the existence of a Session
             * This solves a bug in AccountController.cs where tokens are stored in Session,
             * but somehow the combination of redirects and newly created sessions can sometimes
             * lead to crucial tokens being lost in a session, and crashing the application.
             * If the Session exists beforehand that should not be the case and all should work fine.
             * (note that the actual values that are used below are not important)
             */
            HttpContext.Current.Session["foo"] = "foo";
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                Context.User = userPrincipal;
            }
        }

    }
}

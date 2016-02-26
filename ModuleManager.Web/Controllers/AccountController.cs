using ModuleManager.UserDAL;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.UserDAL.Repositories;
using ModuleManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;

namespace ModuleManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISysteemRolRepository _systeemRolRepository;

        public AccountController(ISysteemRolRepository systeemRolRepository)
        {
            _systeemRolRepository = systeemRolRepository;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
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
                catch (Exception e)
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
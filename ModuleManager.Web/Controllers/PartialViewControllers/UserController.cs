using AutoMapper;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.UserDAL;
using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using ModuleManager.Domain;

namespace ModuleManager.Web.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISysteemRolRepository _systeemRolRepository;
        public UsersController(IUserRepository userRepository, ISysteemRolRepository systeemRolRepository)
        {
            _userRepository = userRepository;
            _systeemRolRepository = systeemRolRepository;
        }

        public ActionResult Create()
        {
            RegistrationVM registrationVM = new RegistrationVM();
            registrationVM.SysteemRollen = _systeemRolRepository.GetAll();


            return PartialView("~/Views/Admin/Users/_Add.cshtml", registrationVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "SysteemRollen")]RegistrationVM registrationVM)
        {

            if (ModelState.IsValid)
            {
                using (var context = new DomainEntities())
                {

                    String hashedPassword = GetSwcSH1(registrationVM.Wachtwoord);

                    var resultlist = context.spRegisterUser(registrationVM.UserNaam, registrationVM.SelectedSysteemRol, registrationVM.Email, registrationVM.Naam);
                    var list = new List<int?>();

                    list = (from element in resultlist select element).ToList();
                    var result = list.SingleOrDefault();

                    if (result == -1)
                    {
                        ViewBag.UsernameError = "User Name already in use, please choose another user name";
                    }
                    else
                    {
                        return Json(new { success = true });
                    }
                }
            }

            registrationVM.SysteemRollen = _systeemRolRepository.GetAll();
            return PartialView("~/Views/Admin/Users/_Add.cshtml", registrationVM);
        }

        public ActionResult Edit(string gebruikersnaam)
        {
            if (gebruikersnaam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userRepository.GetOne(gebruikersnaam);
            Mapper.CreateMap<User, UserEditViewModel>().ForMember(x => x.Wachtwoord, opt => opt.Ignore());
            UserEditViewModel userEditVM = Mapper.Map<UserEditViewModel>(user);

            if (user == null)
            {
                return HttpNotFound();
            }

            userEditVM.OudeUserNaam = user.UserNaam;
            userEditVM.SysteemRollen = _systeemRolRepository.GetAll();
            return PartialView("~/Views/Admin/Users/_Edit.cshtml", userEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel userEditVM)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DomainEntities())
                {

                    String hashedPassword = GetSwcSH1(userEditVM.Wachtwoord);

                    var resultlist = context.spEditUser(userEditVM.OudeUserNaam, userEditVM.UserNaam, userEditVM.SysteemRol, userEditVM.Email, userEditVM.Naam, userEditVM.Blocked);
                    var list = new List<int?>();

                    list = (from element in resultlist select element).ToList();
                    var result = list.SingleOrDefault();

                    if (result == -1)
                    {
                        ViewBag.UsernameError = "User Name already in use, please choose another user name";
                    }
                    else
                    {
                        return Json(new { success = true });
                    }
                }
            }

            userEditVM.SysteemRollen = _systeemRolRepository.GetAll();
            return PartialView("~/Views/Admin/Users/_Edit.cshtml", userEditVM);
        }

        public ActionResult Lock(string gebruikersnaam, bool Blocked)
        {
            if (gebruikersnaam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userRepository.GetOne(gebruikersnaam);
            Mapper.CreateMap<User, UserLockViewModel>();
            UserLockViewModel userVM = Mapper.Map<UserLockViewModel>(user);

            if (user == null)
            {
                return HttpNotFound();
            }
            userVM.Blocked = Blocked;
            return PartialView("~/Views/Admin/Users/_Lock.cshtml", userVM);
        }


        [HttpPost]
        public ActionResult Lock(UserLockViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<UserLockViewModel, User>();
                User user = Mapper.Map<User>(userVM);

                _userRepository.Edit(user);

                return Json(new { success = true });
            }

            return PartialView("~/Views/Admin/Users/_Lock.cshtml", userVM);

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
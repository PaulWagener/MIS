using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.Domain.Interfaces;
using ModuleManager.UserDAL;
using ModuleManager.Web.ViewModels.PartialViewModel;
using ModuleManager.Domain;

namespace ModuleManager.Web.Controllers.PartialViewControllers
{
    [Authorize(Roles = "Admin")]
    public class ModuleBlockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleBlockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Lock(string cursusCode, int schooljaar, bool Blocked)
        {
            if (cursusCode == null || schooljaar == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar);
            if (module == null)
            {
                return HttpNotFound();
            }
            var modulevm = Mapper.Map<Module, ModuleLockViewModel>(module);
            modulevm.Blocked = Blocked;
            return PartialView("~/Views/Admin/CheckModules/_Lock.cshtml", modulevm);
        }


        [HttpPost]
        public ActionResult Lock(ModuleLockViewModel moduleVM)
        {
            if (ModelState.IsValid)
            {
                var module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == moduleVM.CursusCode && m.Schooljaar == moduleVM.Schooljaar);
                if (moduleVM.Blocked)
                {
                    module.Status = "Compleet (gecontroleerd)";
                }
                else
                {
                    module.Status = "Compleet (ongecontroleerd)";
                }

                var value = _unitOfWork.GetRepository<Module>().Edit(module);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }

            return PartialView("~/Views/Admin/CheckModules/_Lock.cshtml", moduleVM);

        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
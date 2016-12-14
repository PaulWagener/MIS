using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;
using ModuleManager.Web.Helpers;

namespace ModuleManager.Web.Controllers.PartialViewControllers
{
    [Authorize(Roles = "Admin")]
    public class ModulesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ModulesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Modules/Create")]
        public ActionResult Create()
        {
            var module = new ModuleCrudViewModel(_unitOfWork);
            module.SetOptions(_unitOfWork);
            return PartialView("~/Views/Admin/Curriculum/Module/_Add.cshtml", module);
        }

        [HttpPost, Route("Modules/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleCrudViewModel module)
        {
            var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
            var schooljaar = schooljaren.Last();

            if (_unitOfWork.Context.Modules.Any(l => l.CursusCode == module.CursusCode && module.Schooljaar == schooljaar.JaarId))
                ModelState.AddModelError("CursusCode", String.Format("De module met de cursus code {0} bestaat al.", module.CursusCode));

            if (!ModelState.IsValid)
            {
                if(module != null)
                    module.SetOptions(_unitOfWork);

                return PartialView("~/Views/Admin/Curriculum/Module/_Add.cshtml", module);
            }

            Module entity = module.ToPoco(_unitOfWork);

            _unitOfWork.Context.Modules.Add(entity);
            _unitOfWork.Context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpGet, Route("Modules/Edit")]
        public ActionResult Edit(string cursusCode, int schooljaar)
        {
            if (cursusCode == null || schooljaar == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Module module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar);

            if (module == null)
            {
                return HttpNotFound();
            }


            var moduleVM = new ModuleCrudViewModel(module, _unitOfWork);

            return PartialView("~/Views/Admin/Curriculum/Module/_Edit.cshtml", moduleVM);
        }

        [HttpPost, Route("Modules/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModuleCrudViewModel module)
        {
            if (!ModelState.IsValid)
            {
                if (module != null)
                    module.SetOptions(_unitOfWork);

                return PartialView("~/Views/Admin/Curriculum/Module/_Edit.cshtml", module);
            }

            try
            {
                Module entity = module.ToPoco(_unitOfWork);
                var value = _unitOfWork.GetRepository<Module>().Edit(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }

        }

        [HttpGet, Route("Modules/Delete")]
        public ActionResult Delete(string cursusCode, int schooljaar)
        {
            if (cursusCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Module module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar);

            if (module == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Module/_Delete.cshtml", module);
        }

        [HttpPost, Route("Modules/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Module entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Module>().Delete(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }

        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;

namespace ModuleManager.Web.Controllers.PartialViewControllers
{
    [Authorize(Roles = "Admin")]
    public class LeerlijnenController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public LeerlijnenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Leerlijnen/Create")]
        public ActionResult Create()
        {
            var leerlijn = new Leerlijn();
            return PartialView("~/Views/Admin/Curriculum/Leerlijn/_Add.cshtml", leerlijn);
        }

        [HttpPost, Route("Leerlijnen/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Leerlijn entity)
        {
            try
            {
                var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
                if (!schooljaren.Any())
                    return Json(new { success = false });
                var schooljaar = schooljaren.Last();

                entity.Schooljaar = schooljaar.JaarId;

                var value = _unitOfWork.GetRepository<Leerlijn>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Leerlijnen/Delete")]
        public ActionResult Delete(string naam, string schooljaar)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Leerlijn leerlijn = _unitOfWork.GetRepository<Leerlijn>().GetOne(new object[] { naam, schooljaar });

            if (leerlijn == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Leerlijn/_Delete.cshtml", leerlijn);
        }

        [HttpPost, Route("Leerlijnen/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Leerlijn entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Leerlijn>().Delete(entity);
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
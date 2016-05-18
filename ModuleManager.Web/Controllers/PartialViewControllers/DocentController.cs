using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.Interfaces;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;

namespace ModuleManager.Web.Controllers.PartialViewControllers
{
    [Authorize(Roles = "Admin")]
    public class DocentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public DocentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Docent/Create")]
        public ActionResult Create()
        {
            var docent = new Docent();
            return PartialView("~/Views/Admin/Curriculum/Docent/_Add.cshtml", docent);
        }

        [HttpPost, Route("Docent/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Docent entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Docent>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Docent/Edit")]
        public ActionResult Edit(int id) {
          if (id <= 0 ) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
          }
          var docent = _unitOfWork.GetRepository<Docent>().GetOne(new object[] { id });

          if (docent == null) {
            return HttpNotFound();
          }

          return PartialView("~/Views/Admin/Curriculum/Docent/_Edit.cshtml", docent);
        }

        [HttpPost, Route("Docent/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Docent entity) {
          try {
            var value = _unitOfWork.GetRepository<Docent>().Edit(entity);
            return value != null ? Json(new {
              success = false,
              strError = value
            }) : Json(new {
              success = true
            });
          } catch (Exception) {
            return Json(new {
              success = false
            });
          }
        }

        [HttpGet, Route("Docent/Delete")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Docent docent = _unitOfWork.GetRepository<Docent>().GetOne(new object[] { id.Value });

            if (docent == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Docent/_Delete.cshtml", docent);
        }

        [HttpPost, Route("Docent/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Docent entity)
        {
            if (entity.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Docent docent = _unitOfWork.GetRepository<Docent>().GetOne(new object[] { entity.Id });

                var value = _unitOfWork.GetRepository<Docent>().Delete(docent);
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
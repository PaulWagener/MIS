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
    public class CompetentiesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompetentiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // TODO: Code uit competentie weghalen, op ID werken.
        [HttpGet, Route("Competenties/Details")]
        public ActionResult Details(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var competentie = _unitOfWork.GetRepository<Competentie>().GetOne(c => c.Naam == code);

            if (competentie == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Competentie/_Details.cshtml", competentie);
        }

        [HttpGet, Route("Competenties/Create")]
        public ActionResult Create()
        {
            var competentie = new CompetentieViewModel();
            return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", competentie);
        }

        // TODO: Competenties creëren niet meer met code.
        [HttpPost, Route("Competenties/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompetentieViewModel competentie)
        {
            if (_unitOfWork.Context.Competenties.Any(c => c.Naam == competentie.Naam))
                ModelState.AddModelError("Code", String.Format("De competentie met de code {0} bestaat al.", competentie.Code));


            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", competentie);


            Competentie entity = competentie.ToPoco();

            try
            {
                var value = _unitOfWork.GetRepository<Competentie>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        // TODO: Competenties niet meer met code laten werken.
        [HttpGet, Route("Competenties/Edit")]
        public ActionResult Edit(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var competentie = _unitOfWork.GetRepository<Competentie>().GetOne(c => c.Naam == code);

            if (competentie == null)
            {
                return HttpNotFound();
            }

            var model = new CompetentieViewModel(competentie);

            return PartialView("~/Views/Admin/Curriculum/Competentie/_Edit.cshtml", model);
        }

        [HttpPost, Route("Competenties/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompetentieViewModel competentie)
        {
            Competentie entity = competentie.ToPoco();

            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", competentie);

            try
            {
                var value = _unitOfWork.GetRepository<Competentie>().Edit(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        // TODO: Delete competentie met Id laten werken.
        [HttpGet, Route("Competenties/Delete")]
        public ActionResult Delete(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Competentie competentie = _unitOfWork.GetRepository<Competentie>().GetOne(c => c.Naam == code);

            if (competentie == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Competentie/_Delete.cshtml", competentie);
        }

        [HttpPost, Route("Competenties/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Competentie entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Competentie>().Delete(entity);
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
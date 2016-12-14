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
    public class OnderdelenController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public OnderdelenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Onderdelen/Create")]
        public ActionResult Create()
        {
            var onderdeel = new OnderdeelViewModel();
            return PartialView("~/Views/Admin/Curriculum/Onderdeel/_Add.cshtml", onderdeel);
        }

        [HttpPost, Route("Onderdelen/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OnderdeelViewModel onderdeel)
        {
            if (_unitOfWork.Context.Onderdeels.Any(l => l.Code == onderdeel.Code))
                ModelState.AddModelError("Code", String.Format("Het onderdeel met de code {0} bestaat al.", onderdeel.Code));


            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Onderdeel/_Add.cshtml", onderdeel);


            Onderdeel entity = onderdeel.ToPoco();

            try
            {
                var value = _unitOfWork.GetRepository<Onderdeel>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Onderdelen/Delete")]
        public ActionResult Delete(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Onderdeel onderdeel = _unitOfWork.GetRepository<Onderdeel>().GetOne(o => o.Code == code);

            if (onderdeel == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Onderdeel/_Delete.cshtml", onderdeel);
        }

        [HttpPost, Route("Onderdelen/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Onderdeel entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Onderdeel>().Delete(entity);
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
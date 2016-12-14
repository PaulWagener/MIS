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
    public class TagsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public TagsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Tags/Create")]
        public ActionResult Create()
        {
            var tag = new TagViewModel();
            return PartialView("~/Views/Admin/Curriculum/Tag/_Add.cshtml", tag);
        }

        [HttpPost, Route("Tags/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel tag)
        {
            if (_unitOfWork.Context.Tags.Any(t => t.Naam == tag.Naam))
                ModelState.AddModelError("Naam", String.Format("De tag met de naam {0} bestaat al.", tag.Naam));


            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Tag/_Add.cshtml", tag);


            Tag entity = tag.ToPoco();

            var value = _unitOfWork.GetRepository<Tag>().Create(entity);
            return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
        }

        [HttpGet, Route("Tags/Delete")]
        public ActionResult Delete(string naam)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tag tag = _unitOfWork.GetRepository<Tag>().GetOne(t => t.Naam == naam);

            if (tag == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Tag/_Delete.cshtml", tag);
        }

        [HttpPost, Route("Tags/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Tag entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Tag>().Delete(entity);
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
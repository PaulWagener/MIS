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
    public class FasesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public FasesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Fases/Create")]
        public ActionResult Create()
        {

            var faseTypes = _unitOfWork.GetRepository<FaseType>().GetAll().ToList();

            var fase = new FaseCrudViewModel()
            {
                FaseTypes = faseTypes.Select(Mapper.Map<FaseType, FaseTypeViewModel>).ToList()
            };

            return PartialView("~/Views/Admin/Curriculum/Fase/_Add.cshtml", fase);
        }

        [HttpPost, Route("Fases/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fase entity)
        {
            try
            {
                var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
                if (!schooljaren.Any())
                    return Json(new { success = false });
                var schooljaar = schooljaren.Last();

                var opleidingen = _unitOfWork.GetRepository<Opleiding>().GetAll().ToArray();
                if (!opleidingen.Any())
                    return Json(new { success = false });
                var opleiding = opleidingen.Last();

                entity.Schooljaar = schooljaar.JaarId;
                entity.Opleiding = opleiding;

                var value = _unitOfWork.GetRepository<Fase>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Fases/Edit")]
        public ActionResult Edit(string naam, string schooljaar, string opleidingsNaam, string opleidingsSchooljaar)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var faseTypes = _unitOfWork.GetRepository<FaseType>().GetAll().ToList();
            var fase = _unitOfWork.GetRepository<Fase>().GetOne(new object[] { naam, schooljaar, opleidingsNaam, opleidingsSchooljaar });

            if (fase == null)
            {
                return HttpNotFound();
            }

            var faseVM = new FaseCrudViewModel()
            {
                FaseType = Mapper.Map<FaseType, FaseTypeViewModel>(fase.FaseType1),
                Naam = fase.Naam,
                Beschrijving = fase.Beschrijving,
                FaseTypes = faseTypes.Select(Mapper.Map<FaseType, FaseTypeViewModel>).ToList()
            };

            return PartialView("~/Views/Admin/Curriculum/Fase/_Edit.cshtml", faseVM);
        }

        [HttpPost, Route("Fases/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fase entity)
        {
            try
            {
                var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
                if (!schooljaren.Any())
                    return Json(new { success = false });
                var schooljaar = schooljaren.Last();

                var opleidingen = _unitOfWork.GetRepository<Opleiding>().GetAll().ToArray();
                if (!opleidingen.Any())
                    return Json(new { success = false });
                var opleiding = opleidingen.Last();

                entity.Schooljaar = schooljaar.JaarId;
                entity.Opleiding = opleiding;
                entity.OpleidingNaam = opleiding.Naam;
                entity.OpleidingSchooljaar = opleiding.Schooljaar;

                var value = _unitOfWork.GetRepository<Fase>().Edit(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Fases/Delete")]
        public ActionResult Delete(string naam, string schooljaar, string opleidingsNaam, string opleidingsSchooljaar)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fase fase = _unitOfWork.GetRepository<Fase>().GetOne(new object[] { naam, schooljaar, opleidingsNaam, opleidingsSchooljaar });

            if (fase == null)
            {
                return HttpNotFound();
            }

            return PartialView("~/Views/Admin/Curriculum/Fase/_Delete.cshtml", fase);
        }

        [HttpPost, Route("Fases/Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Fase entity)
        {
            try
            {
                var value = _unitOfWork.GetRepository<Fase>().Delete(entity);
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
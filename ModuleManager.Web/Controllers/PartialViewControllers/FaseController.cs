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
        public ActionResult Create(FaseCrudViewModel fase)
        {
            if (_unitOfWork.Context.Fases.Any(f => f.Naam == fase.Naam))
                ModelState.AddModelError("Naam", String.Format("De fase met de naam {0} bestaat al.", fase.Naam));


            if (!ModelState.IsValid)
            {
                var faseTypes = _unitOfWork.GetRepository<FaseType>().GetAll().ToList();
                fase.FaseTypes = faseTypes.Select(Mapper.Map<FaseType, FaseTypeViewModel>).ToList();
                return PartialView("~/Views/Admin/Curriculum/Fase/_Add.cshtml", fase);
            }

            Fase entity = fase.ToPoco();
            entity.OpleidingNaam = "Informatica"; //Nog geen opleidingen
            var value = _unitOfWork.GetRepository<Fase>().Create(entity);
            return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
        }

        [HttpGet, Route("Fases/Edit")]
        public ActionResult Edit(string naam)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var faseTypes = _unitOfWork.GetRepository<FaseType>().GetAll().ToList();
            var fase = _unitOfWork.GetRepository<Fase>().GetOne(f => f.Naam == naam);

            if (fase == null)
            {
                return HttpNotFound();
            }

            var faseVM = new FaseCrudViewModel()
            {
                FaseType = fase.FaseType,
                Opleiding = fase.OpleidingNaam,
                Naam = fase.Naam,
                Beschrijving = fase.Beschrijving,
                FaseTypes = faseTypes.Select(Mapper.Map<FaseType, FaseTypeViewModel>).ToList()
            };

            return PartialView("~/Views/Admin/Curriculum/Fase/_Edit.cshtml", faseVM);
        }

        [HttpPost, Route("Fases/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FaseCrudViewModel fase)
        {
            if (!ModelState.IsValid)
            {
                var faseTypes = _unitOfWork.GetRepository<FaseType>().GetAll().ToList();
                fase.FaseTypes = faseTypes.Select(Mapper.Map<FaseType, FaseTypeViewModel>).ToList();
                return PartialView("~/Views/Admin/Curriculum/Fase/_Edit.cshtml", fase);
            }

            try
            {
                Fase entity = fase.ToPoco();
                var value = _unitOfWork.GetRepository<Fase>().Edit(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Fases/Delete")]
        public ActionResult Delete(string naam, string opleidingsNaam)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO: Opleidingsnaam & opleidingsschooljaar uit het request halen.
            Fase fase = _unitOfWork.GetRepository<Fase>().GetOne(f => f.Naam == naam && f.OpleidingNaam == opleidingsNaam);

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
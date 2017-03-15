using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using System.Data.Entity;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;
using ModuleManager.Web.ViewModels.EntityViewModel.Competenties;
using System.Collections.Generic;
using System.Data.Entity.Core;

namespace ModuleManager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompetentiesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompetentiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IQueryable<Competentie> competenties = GetCompetentiesDeepIncluded();

            var viewModels = new List<CompetentieViewModel>();

            foreach (var competentie in competenties)
            {
                viewModels.Add(CreateViewModel(competentie));
            }

            return View(viewModels);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CompetentieViewModel viewModel)
        {
            // TODO: Juiste view returnen
            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", viewModel);

            var competentie = viewModel.ToPoCo();

            try
            {
                _unitOfWork.Context.Competenties.Add(competentie);
                _unitOfWork.Context.SaveChanges();
            }
            catch(EntityException ex)
            {
                ModelState.AddModelError("Naam", String.Format("De competentie met de naam {0} bestaat al.", competentie.Naam));
            }

            // TODO: Juiste view returnen.
            return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", competentie);
        }

        [HttpGet]
        public ActionResult Competentie(int? id)
        {
            var competentie = GetCompetentiesDeepIncluded().SingleOrDefault(c => c.Id == id);

            if (competentie == null)
            {
                return HttpNotFound();
            }

            // TODO: Juiste view returnen.
            return PartialView("~/Views/Admin/Curriculum/Competentie/_Details.cshtml", competentie);
        }

        // TODO: Competenties niet meer met code laten werken.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Competentie(int id, CompetentieViewModel viewModel)
        {
            // TODO: Juiste view returnen
            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", viewModel);

            var competentie = viewModel.ToPoCo();

            try
            {
                _unitOfWork.Context.Competenties.Attach(competentie);
                _unitOfWork.Context.SaveChanges();
            }
            catch (EntityException ex)
            {
                ModelState.AddModelError("Naam", String.Format("De competentie met de naam {0} bestaat al.", competentie.Naam));
            }

            // TODO: Juiste view returnen.
            return PartialView("~/Views/Admin/Curriculum/Competentie/_Add.cshtml", competentie);
        }

        //// TODO: Delete competentie met Id laten werken.
        //[HttpGet, Route("Competenties/Delete")]
        //public ActionResult Delete(string code)
        //{
        //    if (code == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Competentie competentie = _unitOfWork.GetRepository<Competentie>().GetOne(c => c.Naam == code);

        //    if (competentie == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView("~/Views/Admin/Curriculum/Competentie/_Delete.cshtml", competentie);
        //}

        //[HttpPost, Route("Competenties/Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Competentie entity)
        //{
        //    try
        //    {
        //        var value = _unitOfWork.GetRepository<Competentie>().Delete(entity);
        //        return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { success = false });
        //    }

        //}

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        #region Helper methods
        private static CompetentieViewModel CreateViewModel(Competentie competentie)
        {
            var competentieVm = Mapper.Map<CompetentieViewModel>(competentie);

            foreach (var onderdeel in competentie.CompetentieOnderdelen)
            {
                var onderdeelVm = Mapper.Map<CompetentieOnderdeelViewModel>(onderdeel);
                foreach (var kwaliteitskenmerk in onderdeel.Kwaliteitskenmerken)
                {
                    onderdeelVm.Kwaliteitskenmerken.Add(Mapper.Map<KwaliteitskenmerkViewModel>(kwaliteitskenmerk));
                }
                competentieVm.Onderdelen.Add(onderdeelVm);
            }

            return competentieVm;
        }

        private IQueryable<Competentie> GetCompetentiesDeepIncluded()
        {
            return _unitOfWork.Context.Competenties
                                .Include(c => c.CompetentieOnderdelen)
                                .Include(c => c.CompetentieOnderdelen.Select(co => co.Kwaliteitskenmerken));
        }

        #endregion
    }
}
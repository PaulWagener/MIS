﻿using System;
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
            var leerlijn = new LeerlijnViewModel();
            return PartialView("~/Views/Admin/Curriculum/Leerlijn/_Add.cshtml", leerlijn);
        }

        [HttpPost, Route("Leerlijnen/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeerlijnViewModel leerlijn)
        {
            if(_unitOfWork.Context.Leerlijnen.Any(l => l.Naam == leerlijn.Naam))
                ModelState.AddModelError("Naam", String.Format("De leerlijn met de naam {0} bestaat al.", leerlijn.Naam));
            

            if (!ModelState.IsValid)
                return PartialView("~/Views/Admin/Curriculum/Leerlijn/_Add.cshtml", leerlijn);

            try
            {
                Leerlijn entity = leerlijn.ToPoco();
                var value = _unitOfWork.GetRepository<Leerlijn>().Create(entity);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Leerlijnen/Delete")]
        public ActionResult Delete(string naam)
        {
            if (naam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Leerlijn leerlijn = _unitOfWork.GetRepository<Leerlijn>().GetOne(ll => ll.Naam == naam);

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
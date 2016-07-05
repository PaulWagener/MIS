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

        /*
         PK: CursusCode, Schooljaar
         */

        private readonly IUnitOfWork _unitOfWork;
        public ModulesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("Modules/Create")]
        public ActionResult Create()
        {
            var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
            var schooljaar = schooljaren.Last();

            var fases = _unitOfWork.GetRepository<Fase>().GetAll().Where(x => x.Schooljaar == schooljaar.JaarId).ToList();
            var blokken = _unitOfWork.GetRepository<Blok>().GetAll().ToList();
            var icons = _unitOfWork.GetRepository<Icons>().GetAll().ToList();
            var onderdelen = _unitOfWork.GetRepository<Onderdeel>().GetAll().ToList();

            var module = new ModuleCrudViewModel()
            {
                OpleidingsPrefix = "IIIN",
                Fases = fases,
                Blokken = blokken.OrderBy(B => B.BlokId, new NumberWordComparer()),
                Icons = icons,
                Onderdelen = onderdelen
            };

            return PartialView("~/Views/Admin/Curriculum/Module/_Add.cshtml", module);
        }

        [HttpPost, Route("Modules/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleCrudViewModel entity)
        {
            try
            {
                /* Schooljaar */
                var schooljaren = _unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
                if (!schooljaren.Any())
                    return Json(new { success = false });
                var schooljaar = schooljaren.Last();

                /* Studie Punten */
                ICollection<StudiePunten> studiepuntenList = new List<StudiePunten>();
                var studiepunt1 = new StudiePunten()
                {
                    ToetsCode = String.Format("{0}-{1}", entity.Toetscode1Prefix, entity.Toetscode1),
                    EC = entity.Ec1
                };
                studiepuntenList.Add(studiepunt1);

                if (entity.Toetscode2 != null || entity.Toetscode1 == entity.Toetscode2)
                {
                    var studiepunt2 = new StudiePunten()
                    {
                        ToetsCode = String.Format("{0}-{1}", entity.Toetscode2Prefix, entity.Toetscode2),
                        EC = entity.Ec2
                    };
                    studiepuntenList.Add(studiepunt2);
                }

                /* Fases */
                //ICollection<FaseModules> fasesList = new List<FaseModules>();
                //foreach (var _fase in entity.FaseModules)
                //{
                //    var faseSplitted = _fase.Split(',');

                //    var faseModule = new FaseModules()
                //    {
                //        FaseNaam = faseSplitted[0],
                //        FaseSchooljaar = faseSplitted[1],
                //        OpleidingNaam = faseSplitted[2],
                //        OpleidingSchooljaar = faseSplitted[3],
                //        ModuleSchooljaar = schooljaar.JaarId,
                //        ModuleCursusCode = entity.CursusCode,
                //        Blok = entity.Blok
                //    };

                //    fasesList.Add(faseModule);
                //}

                var module = new Module()
                {
                    Schooljaar = schooljaar.JaarId,
                    StudiePunten = studiepuntenList,
                    //FaseModules = fasesList,

                    Naam = entity.Naam,
                    CursusCode = String.Format("{0}-{1}", entity.OpleidingsPrefix, entity.CursusCode),
                    Icon = entity.Icon,
                    Status = "Nieuw",
                    Verantwoordelijke = entity.Verantwoordelijke,
                    OnderdeelCode = entity.Onderdeel
                };

                var value = _unitOfWork.GetRepository<Module>().Create(module);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet, Route("Modules/Edit")]
        public ActionResult Edit(string cursusCode, string schooljaar)
        {
            if (cursusCode == null || schooljaar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Module module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { cursusCode, schooljaar });

            if (module == null)
            {
                return HttpNotFound();
            }

            var fases = _unitOfWork.GetRepository<Fase>().GetAll().Where(x => x.Schooljaar == schooljaar).ToList();
            var blokken = _unitOfWork.GetRepository<Blok>().GetAll().ToList();
            var icons = _unitOfWork.GetRepository<Icons>().GetAll().ToList();
            var onderdelen = _unitOfWork.GetRepository<Onderdeel>().GetAll().ToList();

            var moduleVM = new ModuleCrudViewModel()
            {
                Schooljaar = module.Schooljaar,
                CursusCode = module.CursusCode,
                Naam = module.Naam,
                Icon = module.Icon,
                Verantwoordelijke = module.Verantwoordelijke,
                Onderdeel = module.Onderdeel.Code,
                Toetscode1 = module.StudiePunten.Count > 0 ? module.StudiePunten.ElementAt(0).ToetsCode : null,
                Ec1 = module.StudiePunten.Count > 0 ? module.StudiePunten.ElementAt(0).EC : 0,
                Toetscode2 = module.StudiePunten.Count > 1 ? module.StudiePunten.ElementAt(1).ToetsCode : null,
                Ec2 = module.StudiePunten.Count > 1 ? module.StudiePunten.ElementAt(1).EC : 0,
                FaseModules = module.FaseModules,
                Fases = fases,
                Blokken = blokken.OrderBy(B => B.BlokId, new NumberWordComparer()),
                Icons = icons,
                Onderdelen = onderdelen
            };

            return PartialView("~/Views/Admin/Curriculum/Module/_Edit.cshtml", moduleVM);
        }

        [HttpPost, Route("Modules/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModuleCrudViewModel entity)
        {
            try
            {
                 Module module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { entity.CursusCode, entity.Schooljaar });


                /* Studie Punten */
                ICollection<StudiePunten> studiepuntenList = new List<StudiePunten>();
                var studiepunt1 = new StudiePunten()
                {
                    ToetsCode = entity.Toetscode1,
                    EC = entity.Ec1
                };
                studiepuntenList.Add(studiepunt1);

                if (entity.Toetscode2 != null || entity.Toetscode1 == entity.Toetscode2)
                {
                    var studiepunt2 = new StudiePunten()
                    {
                        ToetsCode = entity.Toetscode2,
                        EC = entity.Ec2
                    };
                    studiepuntenList.Add(studiepunt2);
                }


                /* Fases */
                //ICollection<FaseModules> fasesList = new List<FaseModules>();
                //foreach (var _fase in entity.SelectedFases)
                //{
                //    var faseSplitted = _fase.Split(',');

                //    var faseModule = new FaseModules()
                //    {
                //        FaseNaam = faseSplitted[0],
                //        FaseSchooljaar = faseSplitted[1],
                //        OpleidingNaam = faseSplitted[2],
                //        OpleidingSchooljaar = faseSplitted[3],
                //        ModuleSchooljaar = entity.Schooljaar,
                //        ModuleCursusCode = entity.CursusCode,
                //        Blok = entity.Blok
                //    };

                //    fasesList.Add(faseModule);
                //}


                module.Naam = entity.Naam;
                module.Icon = entity.Icon;
                module.Status = "Nieuw";
                module.Verantwoordelijke = entity.Verantwoordelijke;
                module.OnderdeelCode = entity.Onderdeel;
                
                module.StudiePunten.Clear();
                module.FaseModules.Clear();
                var value = _unitOfWork.GetRepository<Module>().Edit(module);

                //module.FaseModules = fasesList;
                module.StudiePunten = studiepuntenList;
                value = _unitOfWork.GetRepository<Module>().Edit(module);
                return value != null ? Json(new { success = false, strError = value }) : Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }

        }

        [HttpGet, Route("Modules/Delete")]
        public ActionResult Delete(string cursusCode, string schooljaar)
        {
            if (cursusCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Module module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { cursusCode, schooljaar });

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
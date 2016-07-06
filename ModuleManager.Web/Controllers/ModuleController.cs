using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System.IO;
using ModuleManager.Web.ViewModels.RequestViewModels;
using System.Collections.Generic;
using WebGrease;


namespace ModuleManager.Web.Controllers
{

    public class ModuleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExporterService<Module> _moduleExporterService;
        private readonly IFilterSorterService<Module> _filterSorterService;

        private Api.ModuleController _moduleApi { get; set; }

        public ModuleController(IExporterService<Module> moduleExporterService, IFilterSorterService<Module> filterSorterService, IUnitOfWork unitOfWork)
        {
            _moduleExporterService = moduleExporterService;
            _filterSorterService = filterSorterService;
            _unitOfWork = unitOfWork;
            _moduleApi = new Api.ModuleController(filterSorterService, unitOfWork);
        }

        /// <summary>
        /// Methode om de hoofd module overzicht's pagina op te bouwen en te sturen naar de client.
        /// </summary>
        /// <returns>De hoofd module overzicht pagina</returns>
        [HttpGet, Route("Module/Overview")]
        public ActionResult Overview()
        {

            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);
            //Collect the possible filter options the user can choose.

            var filterOptions = new FilterOptionsViewModel();
            filterOptions.AddBlokken(_unitOfWork.GetRepository<Blok>().GetAll());
            filterOptions.AddCompetenties(_unitOfWork.GetRepository<Competentie>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddECs();
            filterOptions.AddFases(_unitOfWork.GetRepository<Fase>().GetAll().ToList());
            filterOptions.AddLeerjaren(_unitOfWork.GetRepository<Schooljaar>().GetAll());
            filterOptions.AddLeerlijnen(_unitOfWork.GetRepository<Leerlijn>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddTags(_unitOfWork.GetRepository<Tag>().GetAll());


            //Construct the ViewModel.
            var moduleOverviewVm = new ModuleOverviewViewModel
            {
                FilterOptions = filterOptions,
            };

            return View(moduleOverviewVm);
        }

        [HttpGet, Route("Module/Details/{schooljaar}/{cursusCode}")]
        public ActionResult Details(string schooljaar, string cursusCode)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { cursusCode, schooljaar });
            var modVm = Mapper.Map<Module, ModuleViewModel>(module);
            return View(modVm);
        }

        [Authorize]
        [HttpGet, Route("Module/Edit/{schooljaar}/{cursusCode}")]
        public ActionResult Edit(string schooljaar, string cursusCode)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { cursusCode, schooljaar });

            var moduleEditViewModel = new ModuleEditViewModel
            {
                Module = Mapper.Map<Module, ModuleViewModel>(module),
                Options = new ModuleEditOptionsViewModel(_unitOfWork)
            };

            moduleEditViewModel.Module.IsCompleted = (module.Status != "Compleet (ongecontroleerd)");
            return View(moduleEditViewModel);
        }

        [Authorize]
        [HttpPost, Route("Module/Edit")]
        public ActionResult Edit(ModuleEditViewModel moduleVm)
        {
            if (!ModelState.IsValid)
            {
                moduleVm.Options = new ModuleEditOptionsViewModel(_unitOfWork);
                return View(moduleVm);
            }

            using(var context = new DomainEntities()){

                //Ophalen originele module
                var module = context.Modules.Find(new object[] { moduleVm.Module.CursusCode, moduleVm.Module.Schooljaar });

                //simpel fields
                module.Beschrijving = moduleVm.Module.Beschrijving;

                //### many to many ###
                //#leerlijnen
                module.Leerlijnen.Clear();
                var leerlijnen = new List<Leerlijn>();
                foreach(var leerlijn in moduleVm.Module.Leerlijn)
                {
                    leerlijnen.Add(context.Leerlijnen.Find(new object[] { leerlijn.Naam,leerlijn.Schooljaar }));
                }
                module.Leerlijnen = leerlijnen;

                //#tags
                module.Tags.Clear();
                var tags = new List<Tag>();
                foreach (var tag in moduleVm.Module.Tag)
                {
                    tags.Add(context.Tags.Find(new object[] { tag.Naam }));
                }
                module.Tags = tags;

                //#modules voorkennis
                module.Voorkennis.Clear();
                var voorkennis = new List<Module>();
                foreach (var moduleVoorkennis in moduleVm.Module.VoorkennisModules)
                {
                    voorkennis.Add(context.Modules.Find(moduleVoorkennis.CursusCode, moduleVoorkennis.Schooljaar));
                }
                module.Voorkennis = voorkennis;

                //#modules docenten
                module.Docenten.Clear();
                var docenten = new List<Docent>();
                foreach (var docent in moduleVm.Module.Docenten)
                {
                    docenten.Add(context.Docenten.Find(docent.Id));
                }
                module.Docenten = docenten;

  

                module.Leerdoelen.Clear();
                module.Leerdoelen = moduleVm.Module.Leerdoelen.Where(s => !s.isDeleted.GetValueOrDefault()).Select(l => l.ToPoco(context)).ToList();
                module.Leermiddelen.Clear();
                module.Leermiddelen = moduleVm.Module.Leermiddelen.Where(s => !s.isDeleted.GetValueOrDefault()).Select(l => l.ToPoco(context)).ToList();
                module.StudieBelastingen.Clear();
                context.SaveChanges(); //jammer maar nodig
                module.StudieBelastingen = moduleVm.Module.StudieBelasting.Where(s => !s.isDeleted.GetValueOrDefault()).Select(s => s.ToPoco(context)).ToList();
                module.Weekplanningen.Clear();
                module.Weekplanningen = moduleVm.Module.Weekplanning.Where(s => !s.isDeleted.GetValueOrDefault()).Select(w => w.ToPoco(context)).ToList();
                module.Beoordelingen.Clear();
                module.Beoordelingen = moduleVm.Module.Beoordelingen.Where(s => !s.isDeleted.GetValueOrDefault()).Select(b => b.ToPoco(context)).ToList();

                //koppel tabellen
                module.ModuleWerkvormen.Clear();
                module.ModuleWerkvormen = moduleVm.Module.ModuleWerkvorm.Select(wv => wv.ToPoco(context)).ToList();
                module.ModuleCompetenties.Clear();
                module.ModuleCompetenties = moduleVm.Module.ModuleCompetentie.Select(mc => mc.ToPoco(context, module)).Where(mc => mc.CompetentieCode != null).ToList();

                if(moduleVm.Module.IsCompleted)
                {
                    //Module valideren
                    module.Status = "Compleet (ongecontroleerd)";
                } else
                {
                    module.Status = "Incompleet";
                }

                context.SaveChanges();
            }

            if (moduleVm.AfterSubmit == "stay")
            {
                return RedirectToAction("Edit/" + moduleVm.Module.Schooljaar + "/" + moduleVm.Module.CursusCode);
            }
                
            return RedirectToAction("Details/" + moduleVm.Module.Schooljaar + "/" + moduleVm.Module.CursusCode);
        }

        //PDF Download Code

        [HttpGet, Route("Module/Export/{schooljaar}/{cursusCode}")]
        public FileStreamResult ExportSingleModule(string schooljaar, string cursusCode)
        {
            Stream fStream = _moduleExporterService.ExportAsStream(_unitOfWork.GetRepository<Module>().GetOne(new object[] { cursusCode, schooljaar }));

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");

            return new FileStreamResult(fStream, "application/pdf");
        }


        [HttpPost]
        public ActionResult ExportAllModules(ExportArgumentsViewModel value)
        {
            var modules = _unitOfWork.GetRepository<Module>().GetAll();

            if (!User.Identity.IsAuthenticated) 
            {
                modules = modules.Where(element => element.Status.Equals("Compleet (gecontroleerd)"));
            }

            var arguments = new ModuleFilterSorterArguments
            {
                CompetentieFilters = value.Filters.Competenties,
                TagFilters = value.Filters.Tags,
                LeerlijnFilters = value.Filters.Leerlijnen,
                FaseFilters = value.Filters.Fases,
                BlokFilters = value.Filters.Blokken,
                ZoektermFilter = value.Filters.Zoekterm,
                LeerjaarFilter = value.Filters.Leerjaar
            };

            var queryPack = new ModuleQueryablePack(arguments, modules.AsQueryable());
            modules = _filterSorterService.ProcessData(queryPack);

            var exportArguments = new ModuleExportArguments
            {
                ExportCursusCode = value.Export.CursusCode,
                ExportNaam = value.Export.Naam,
                ExportBeschrijving = value.Export.Beschrijving,
                ExportAlgInfo = value.Export.AlgemeneInformatie,
                ExportStudieBelasting = value.Export.Studiebelasting,
                ExportOrganisatie = value.Export.Organisatie,
                ExportWeekplanning = value.Export.Weekplanning,
                ExportBeoordeling = value.Export.Beoordeling,
                ExportLeermiddelen = value.Export.Leermiddelen,
                ExportLeerdoelen = value.Export.Leerdoelen,
                ExportCompetenties = value.Export.Competenties,
                ExportLeerlijnen = value.Export.Leerlijnen,
                ExportTags = value.Export.Tags
            };

            var exportablePack = new ModuleExportablePack(exportArguments, modules);

            BufferedStream fStream = _moduleExporterService.ExportAllAsStream(exportablePack);

            string expByName = User.Identity.Name;
            if (expByName == null || expByName.Equals(""))
            {
                expByName = "download";
            }

            return new FileStreamResult(fStream, "application/pdf");

        
        }


        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
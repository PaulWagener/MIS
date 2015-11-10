using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.Interfaces;
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

        public ModuleController(IExporterService<Module> moduleExporterService, IFilterSorterService<Module> filterSorterService, IUnitOfWork unitOfWork)
        {
            _moduleExporterService = moduleExporterService;
            _filterSorterService = filterSorterService;
            _unitOfWork = unitOfWork;
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
            filterOptions.AddFases(_unitOfWork.GetRepository<Fase>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddLeerjaren(_unitOfWork.GetRepository<Schooljaar>().GetAll());
            filterOptions.AddLeerlijnen(_unitOfWork.GetRepository<Leerlijn>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddTags(_unitOfWork.GetRepository<Tag>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));

            //Construct the ViewModel.
            var moduleOverviewVm = new ModuleOverviewViewModel
            {
                FilterOptions = filterOptions
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
            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll();
            var tags = _unitOfWork.GetRepository<Tag>().GetAll();
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll();
            var werkvormen = _unitOfWork.GetRepository<Werkvorm>().GetAll();
            var toetsvormen = _unitOfWork.GetRepository<Toetsvorm>().GetAll();
            var modules = _unitOfWork.GetRepository<Module>().GetAll();
            var niveaus = _unitOfWork.GetRepository<Niveau>().GetAll();
            var docenten = _unitOfWork.GetRepository<Docent>().GetAll();

            var isComplete = true;
            if (module.Status != "Compleet (ongecontroleerd)")
            {
                isComplete = false;
            }

            var isLockedForEdit = false;
            if (module.Status == "Compleet (gecontroleerd)")
            {
                isLockedForEdit = true;
            }

            var moduleEditViewModel = new ModuleEditViewModel
            {
                Module = Mapper.Map<Module, ModuleViewModel>(module),
                Options = new ModuleEditOptionsViewModel
                {
                    Competenties = competenties.Select(Mapper.Map<Competentie, CompetentieViewModel>).ToList(),
                    Leerlijnen = leerlijnen.Select(Mapper.Map<Leerlijn, LeerlijnViewModel>).ToList(),
                    Tags = tags.Select(Mapper.Map<Tag, TagViewModel>).ToList(),
                    Toetsvormen = toetsvormen.Select(Mapper.Map<Toetsvorm, ToetsvormViewModel>).ToList(),
                    VoorkennisModules = modules.Select(Mapper.Map<Module, ModuleVoorkennisViewModel>).ToList(),
                    Werkvormen = werkvormen.Select(Mapper.Map<Werkvorm, WerkvormViewModel>).ToList(),
                    Niveaus = niveaus.Select(Mapper.Map<Niveau, NiveauViewModel>).ToList(),
                    Docenten = docenten.Select(Mapper.Map<Docent, DocentViewModel>).ToList()
                }
            };

            return View(moduleEditViewModel);
        }

        [Authorize]
        [HttpPost, Route("Module/Edit")]
        public ActionResult Edit(ModuleEditViewModel moduleVm)
        {
            IGenericRepository<Module> moduleRepo = _unitOfWork.GetRepository<Module>();
            var moduleToEdit = moduleRepo.GetOne(new object[] { moduleVm.Module.CursusCode, moduleVm.Module.Schooljaar });
            
            moduleToEdit.Beschrijving = moduleVm.Module.Beschrijving;
            moduleToEdit.Docent = moduleVm.Module.MapToDocent();
            moduleToEdit.FaseModules = moduleVm.Module.MapToFaseModules();
            moduleToEdit.Leerdoelen = moduleVm.Module.MapToLeerdoelen();
            moduleToEdit.Leerlijn = moduleVm.Module.MapToLeerlijn();
            moduleToEdit.Leermiddelen = moduleVm.Module.MapToLeermiddelen();
            moduleToEdit.ModuleCompetentie = moduleVm.Module.MapToModuleCompetentie();
            moduleToEdit.ModuleWerkvorm = moduleVm.Module.MapToModuleWerkvorm();
            moduleToEdit.StudieBelasting = moduleVm.Module.MapToStudieBelasting();
            //moduleToEdit.StudiePunten = moduleVm.Module.MapToStudiePunten();
            moduleToEdit.Tag = moduleVm.Module.MapToTag();
            moduleToEdit.Weekplanning = moduleVm.Module.MapToWeekplanning();

            var voorkennisModules = new List<Module>();
            foreach (var voorkennisModule in moduleVm.Module.Module2)
            {
                Module voorMod = moduleRepo.GetOne(new object[] { voorkennisModule.CursusCode, voorkennisModule.Schooljaar });

                voorkennisModules.Add(voorMod);
            }

            moduleToEdit.Module2 = voorkennisModules;

            if (moduleVm.Module.IsCompleted)
            {
                moduleToEdit.Status = "Compleet (ongecontroleerd)";
            }

             moduleRepo.Edit(moduleToEdit);
             _unitOfWork.Context.SaveChanges();
            _unitOfWork.Dispose();

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


        [HttpPost, Route("Module/ExportAll")]
        public ActionResult ExportAllModules(ExportArgumentsViewModel value)
        {
            var modules = _unitOfWork.GetRepository<Module>().GetAll();

            if (!User.Identity.IsAuthenticated) 
            {
                modules = modules.Where(element => element.Status.Equals("Compleet (gecontroleerd)"));
            }

            ICollection<string> competentieFilters = null;
            if (value.Filters.Competenties.First() != null)
                competentieFilters = value.Filters.Competenties;

            ICollection<string> tagFilters = null;
            if (value.Filters.Tags.First() != null)
                tagFilters = value.Filters.Tags;

            ICollection<string> leerlijnFilters = null;
            if (value.Filters.Leerlijnen.First() != null)
                leerlijnFilters = value.Filters.Leerlijnen;

            ICollection<string> faseFilters = null;
            if (value.Filters.Fases.First() != null)
                faseFilters = value.Filters.Fases;

            ICollection<string> blokFilters = null;
            if (value.Filters.Blokken.First() != null)
                blokFilters = value.Filters.Blokken;

            string zoektermFilter = null;
            if (value.Filters.Zoekterm != null)
                zoektermFilter = value.Filters.Zoekterm;

            string leerjaarFilter = null;
            if (value.Filters.Leerjaar != null)
                leerjaarFilter = value.Filters.Leerjaar;

            var arguments = new ModuleFilterSorterArguments
            {
                CompetentieFilters = competentieFilters,
                TagFilters = tagFilters,
                LeerlijnFilters = leerlijnFilters,
                FaseFilters = faseFilters,
                BlokFilters = blokFilters,
                ZoektermFilter = zoektermFilter,
                LeerjaarFilter = leerjaarFilter
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

            string saveTo = DateTime.Now.ToString("yyyy-MM-dd") + "_" + expByName;
            Session[saveTo] = fStream;

            //Return the filename under which you can retrieve it from Session data.
            //Ajax/jQuery will then parse that string, and redirect to /Module/Export/All/{saveTo}
            //This redirect will be caught in the controller action below here.
            return Json(saveTo);
        }

        [HttpGet, Route("Module/Export/All/{loadFrom}")]
        public FileStreamResult GetExportAllModules(string loadFrom)
        {
            BufferedStream fStream = Session[loadFrom] as BufferedStream;
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");
            Session[loadFrom] = null;

            return new FileStreamResult(fStream, "application/pdf");
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
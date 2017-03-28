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
using ModuleManager.Web.Helpers;
using System.Data;

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
            filterOptions.AddBlokken(_unitOfWork.GetRepository<Blok>().GetAll().OrderBy(blok => blok.BlokId));
            filterOptions.AddCompetenties(_unitOfWork.GetRepository<Competentie>().GetAll());
            filterOptions.AddECs();
            filterOptions.AddFases(_unitOfWork.GetRepository<Fase>().GetAll().ToList());
            filterOptions.AddLeerjaren(_unitOfWork.GetRepository<Schooljaar>().GetAll());
            filterOptions.AddLeerlijnen(_unitOfWork.GetRepository<Leerlijn>().GetAll());
            filterOptions.AddTags(_unitOfWork.GetRepository<Tag>().GetAll());


            //Construct the ViewModel.
            var moduleOverviewVm = new ModuleOverviewViewModel
            {
                FilterOptions = filterOptions,
            };

            return View(moduleOverviewVm);
        }

        [HttpGet, Route("Module/Details/{schooljaar}/{cursusCode}")]
        public ActionResult Details(int schooljaar, string cursusCode)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar);
            var moduleVM = Mapper.Map<Module, ModuleViewModel>(module);
            return View(moduleVM);
        }

        [Authorize]
        [HttpGet, Route("Module/Edit/{schooljaar}/{cursusCode}")]
        public ActionResult Edit(int schooljaar, string cursusCode)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar);

            var moduleEditViewModel = new ModuleEditViewModel
            {
                Module = Mapper.Map<Module, ModuleViewModel>(module),
                Options = new ModuleEditOptionsViewModel(_unitOfWork)
            };

            return View(moduleEditViewModel);
        }

        private static IList<T> EmptyIfNull<T>(Func<IList<T>> f) => f() ?? new List<T>();
        private static IList<T> EmptyIfNull<T>(IList<T> l) => l ?? new List<T>();

        private static void UpdateModuleWerkvormenFromViewModel(DomainEntities context, ICollection<ModuleWerkvormViewModel> werkvormen, Module module)
        {

        }

        [Authorize]
        [HttpPost, Route("Module/Edit/{schooljaar}/{cursusCode}")]
        public ActionResult Edit(int schooljaar, string cursusCode, ModuleEditViewModel moduleVm)
        {
            moduleVm.Options = new ModuleEditOptionsViewModel(_unitOfWork);

            moduleVm.Module.Validate(ModelState);
            if (!ModelState.IsValid)
            {
                return View(moduleVm);
            }

            var module = _unitOfWork.Context.Modules.FirstOrDefault(m => m.CursusCode == moduleVm.Module.CursusCode && m.Schooljaar == moduleVm.Module.Schooljaar);
            module.Beschrijving = moduleVm.Module.Beschrijving;

            // Many to Many
            module.Docenten.ClearAndFill<Docent>(_unitOfWork.Context.Docenten.ToList().Where(d => moduleVm.Module.Docenten.Any(tmp => tmp.Id == d.Id)));
            module.Voorkennismodules.ClearAndFill<Module>(_unitOfWork.Context.Modules.ToList().Where(m => moduleVm.Module.Voorkennis.Any(temp => temp.CursusCode == m.CursusCode && temp.Schooljaar == m.Schooljaar)));
            module.Tags.ClearAndFill<Tag>(_unitOfWork.Context.Tags.ToList().Where(t => moduleVm.Module.Tags.Any(temp => temp.Naam == t.Naam)));
            module.Leerlijnen.ClearAndFill<Leerlijn>(_unitOfWork.Context.Leerlijnen.ToList().Where(ll => moduleVm.Module.Leerlijnen.Any(temp => temp.Naam == ll.Naam)));

            // Enkel deze module
            module.Leermiddelen.ClearAndFill(moduleVm.Module.Leermiddelen);
            module.ModuleWerkvormen.ClearAndFill(moduleVm.Module.ModuleWerkvormen);
            module.StudieBelastingen.ClearAndFill(moduleVm.Module.StudieBelastingen);
            module.Weekplanningen.ClearAndFill(moduleVm.Module.Weekplanningen);

            // Special cases: Lijst met verwijzingen.
            UpdateModuleLeerdoelen(moduleVm, module);

            _unitOfWork.Context.SaveChanges();
            

            if (moduleVm.AfterSubmit == "stay")
            {
                return RedirectToAction("Edit/" + moduleVm.Module.Schooljaar + "/" + moduleVm.Module.CursusCode);
            }

            return RedirectToAction("Details/" + moduleVm.Module.Schooljaar + "/" + moduleVm.Module.CursusCode);
        }

        private void UpdateModuleLeerdoelen(ModuleEditViewModel moduleVm, Module module)
        {
            foreach (var leerdoel in module.Leerdoelen.ToList())
            {
                _unitOfWork.Context.Leerdoelen.Remove(leerdoel);
            }

            if (moduleVm.Module.Leerdoelen != null)
            {
                var allKwaliteitskenmerken = _unitOfWork.Context.Kwaliteitskenmerken.ToList();
                foreach (var leerdoel in moduleVm.Module.Leerdoelen)
                {
                    var entity = new Leerdoel()
                    {
                        Beschrijving = leerdoel.Beschrijving,
                        CursusCode = module.CursusCode,
                        Schooljaar = module.Schooljaar
                    };

                    foreach (var kk in leerdoel.Kwaliteitskenmerken)
                    {
                        entity.Kwaliteitskenmerken.Add(allKwaliteitskenmerken.First(item => item.Id == kk.Id));
                    }
                    module.Leerdoelen.Add(entity);
                }
            }
        }

        //PDF Download Code

        [HttpGet, Route("Module/Export/{schooljaar}/{cursusCode}")]
        public FileStreamResult ExportSingleModule(int schooljaar, string cursusCode)
        {
            Stream fStream = _moduleExporterService.ExportAsStream(_unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == cursusCode && m.Schooljaar == schooljaar));

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");

            return new FileStreamResult(fStream, "application/pdf");
        }


        [HttpPost]
        public ActionResult ExportAllModules(ExportArgumentsViewModel value)
        {
            var arguments = new ModuleFilterSorterArguments
            {
                CompetentieFilters = value.Filters.Competenties,
                TagFilters = value.Filters.Tags,
                LeerlijnFilters = value.Filters.Leerlijnen,
                FaseFilters = value.Filters.Fases,
                BlokFilters = value.Filters.Blokken,
                ZoektermFilter = value.Filters.Zoekterm,
                LeerjaarFilter = value.Filters.Leerjaar,
                StatusFilter = User.Identity.IsAuthenticated ? null : "Compleet (gecontroleerd)",
                Offset = 0,
                Limit = null
            };

            int totalRecordCount;
            var modules = _filterSorterService.ProcessData(new ModuleQueryablePack(arguments, _unitOfWork.Context.Modules), out totalRecordCount);

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
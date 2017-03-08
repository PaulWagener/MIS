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

            using (var context = new DomainEntities())
            {

                //Ophalen originele module
                var module = context.Modules.FirstOrDefault(m => m.CursusCode == moduleVm.Module.CursusCode && m.Schooljaar == moduleVm.Module.Schooljaar);

                //simpel fields
                module.Beschrijving = moduleVm.Module.Beschrijving;

                //### many to many ###
                //#leerlijnen
                module.Leerlijnen.Clear();
                var leerlijnen = new List<Leerlijn>();
                foreach (var leerlijn in moduleVm.Module.Leerlijnen)
                {
                    leerlijnen.Add(context.Leerlijnen.FirstOrDefault(ll => ll.Naam == leerlijn.Naam));
                }
                module.Leerlijnen = leerlijnen;

                //#tags
                module.Tags.Clear();
                var tags = new List<Tag>();
                foreach (var tag in moduleVm.Module.Tags)
                {
                    tags.Add(context.Tags.FirstOrDefault(t => t.Naam == tag.Naam));
                }
                module.Tags = tags;

                //#modules voorkennis
                module.Voorkennis.Clear();
                var voorkennis = new List<Module>();
                foreach (var moduleVoorkennis in moduleVm.Module.Voorkennis)
                {
                    voorkennis.Add(context.Modules.FirstOrDefault(m => m.CursusCode == moduleVoorkennis.CursusCode && m.Schooljaar == moduleVoorkennis.Schooljaar));
                }
                module.Voorkennis = voorkennis;

                //#modules docenten
                module.Docenten.Clear();
                var docenten = new List<Docent>();
                foreach (var docent in moduleVm.Module.Docenten)
                {
                    docenten.Add(context.Docenten.FirstOrDefault(d => d.Id == docent.Id));
                }
                module.Docenten = docenten;


                // Implementation details of MS's EntityFramework have reached our code. 
                //
                // We need to load every navigation property before the corresponding property
                // may be updated via assignment. If not loaded, entities that should no longer
                // be part of a relationship will not be deleted.
                //
                // This was previously achieved by calling Clear on the ICollection<TEntity>.
                // However that method is dangerous, as the ViewModels' ToPoco
                // methods might then return a deleted object (which may no longer be used
                // in relationships).
                //
                // Also, there is no type safe way to do this, I instead have to pass
                // strings, mitigating the type system, making this code even
                // more prone to runtime errors. 
                var entry = context.Entry(module);
                foreach (var navProperty in new string[] { "Leerdoelen", "Leermiddelen", "StudieBelastingen", "Weekplanningen", "Beoordelingen", "ModuleWerkvormen", "ModuleCompetenties" })
                    entry.Collection(navProperty).Load();

                module.Leerdoelen = EmptyIfNull(() => moduleVm.Module.Leerdoelen).Select(l => l.ToPoco(context)).ToList();
                module.Leermiddelen = EmptyIfNull(() => moduleVm.Module.Leermiddelen).Select(l => l.ToPoco(context)).ToList();
                module.StudieBelastingen = EmptyIfNull(() => moduleVm.Module.StudieBelastingen).Select(s => s.ToPoco(context)).ToList();
                module.Weekplanningen = EmptyIfNull(() => moduleVm.Module.Weekplanningen).Select(w => w.ToPoco(context)).ToList();
                module.Beoordelingen = EmptyIfNull(() => moduleVm.Module.Beoordelingen).Select(b => b.ToPoco(context)).ToList();
                module.ModuleWerkvormen = EmptyIfNull(moduleVm.Module.ModuleWerkvormen).Select(wv => wv.ToPoco(context)).ToList();
                // TODO: Leerdoelen/Modulecompetenties weer goed vullen.
                //module.ModuleCompetenties = EmptyIfNull(() => moduleVm.Module.ModuleCompetenties).Select(mc => mc.ToPoco(context, module)).Where(mc => mc.CompetentieCode != null).ToList();

                module.Status1 = context.Status.SingleOrDefault(s => s.Status1 == moduleVm.Module.Status);

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
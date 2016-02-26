using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.ExtensionMethods;
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
            filterOptions.AddFases(_unitOfWork.GetRepository<Fase>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddLeerjaren(_unitOfWork.GetRepository<Schooljaar>().GetAll());
            filterOptions.AddLeerlijnen(_unitOfWork.GetRepository<Leerlijn>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));
            filterOptions.AddTags(_unitOfWork.GetRepository<Tag>().GetAll().Where(src => src.Schooljaar.Equals(maxSchooljaar)));


            //Construct the ViewModel.
            var moduleOverviewVm = new ModuleOverviewViewModel
            {
                FilterOptions = filterOptions,
                ModuleViewModels = _moduleApi.GetAll(),
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

            //new MultiSelectList(Model.Options.Competenties, "Naam", "Naam", (from c in Model.Module.ModuleCompetentie where c.Niveau == Model.Options.Niveaus.ElementAt(i).Niveau1 select c.Competentie.Naam).ToList()), new { @id = Model.Options.Niveaus.ElementAt(i).Niveau1 + "-select", @multiple = "form-control", @class = "form-control" })</td>
            MultiSelectList competetentieVM = new MultiSelectList(competenties, "Code", "Naam");

            var moduleEditViewModel = new ModuleEditViewModel
            {
                Module = Mapper.Map<Module, ModuleViewModel>(module),
                Options = new ModuleEditOptionsViewModel
                {
                    Competenties = competetentieVM,
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
            using(var context = new DomainContext()){

                //Ophalen originele module
                var module = context.Module.Find(new object[] { moduleVm.Module.CursusCode, moduleVm.Module.Schooljaar });

                //simpel fields
                module.Beschrijving = moduleVm.Module.Beschrijving;

                //### many to many ###
                //#leerlijnen
                module.Leerlijn.Clear();
                var leerlijnen = new List<Leerlijn>();
                foreach(var leerlijn in moduleVm.Module.Leerlijn)
                {
                    leerlijnen.Add(context.Leerlijn.Find(new object[] { leerlijn.Naam,leerlijn.Schooljaar }));
                }
                module.Leerlijn = leerlijnen;

                //#tags
                module.Tag.Clear();
                var tags = new List<Tag>();
                foreach (var tag in moduleVm.Module.Tag)
                {
                    tags.Add(context.Tag.Find(new object[] { tag.Naam, tag.Schooljaar }));
                }
                module.Tag = tags;

                //#modules voorkennis
                module.Voorkennis.Clear();
                var voorkennis = new List<Module>();
                foreach (var moduleVoorkennis in moduleVm.Module.VoorkennisModules)
                {
                    voorkennis.Add(context.Module.Find(moduleVoorkennis.CursusCode, moduleVoorkennis.Schooljaar));
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
                module.Leerdoelen = moduleVm.Module.Leerdoelen.Select(l => l.ToPoco(context)).ToList();
                module.Leermiddelen.Clear();
                module.Leermiddelen = moduleVm.Module.Leermiddelen.Select(l => l.ToPoco(context)).ToList();
                module.StudieBelasting.Clear();
                context.SaveChanges(); //jammer maar nodig
                module.StudieBelasting = moduleVm.Module.StudieBelasting.Select(s => s.ToPoco(context)).ToList();
                module.Weekplanning.Clear();
                module.Weekplanning = moduleVm.Module.Weekplanning.Select(w => w.ToPoco(context)).ToList();
                module.Beoordelingen.Clear();
                module.Beoordelingen = moduleVm.Module.Beoordelingen.Select(b => b.ToPoco(context)).ToList();

                //koppel tabellen
                module.ModuleWerkvorm.Clear();
                module.ModuleWerkvorm = moduleVm.Module.ModuleWerkvorm.Select(wv => wv.ToPoco(context)).ToList();
                module.ModuleCompetentie.Clear();
                module.ModuleCompetentie = moduleVm.Module.ModuleCompetentie.Select(mc => mc.ToPoco(context, module)).Where(mc => mc.CompetentieCode != null).ToList();

                if(moduleVm.Module.IsCompleted)
                {
                    //Module valideren
                    module.Status = "Compleet (gecontroleerd)";
                }

                context.SaveChanges();
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
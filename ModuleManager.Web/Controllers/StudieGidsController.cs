using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.ViewModels;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System.Collections.Generic;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces;
using System.IO;
using System.Web.UI.WebControls.Expressions;
using System.Data.Entity;

namespace ModuleManager.Web.Controllers
{
    public class StudieGidsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExporterService<Competentie> _competentieExporterService;
        private readonly IExporterService<Leerlijn> _leerlijnExporterService;
        private readonly IExporterService<FaseType> _lessenTabelExporterService;

        public StudieGidsController(IUnitOfWork unitOfWork, IExporterService<Competentie> competentieExporterService, IExporterService<Leerlijn> leerlijnExporterService, IExporterService<FaseType> lessenTabelExporterService)
        {
            _unitOfWork = unitOfWork;
            _competentieExporterService = competentieExporterService;
            _leerlijnExporterService = leerlijnExporterService;
            _lessenTabelExporterService = lessenTabelExporterService;
        }

        // GET: Table
        [HttpGet]
        public ActionResult Index()
        {
            var maxSchooljaar = _unitOfWork.Context.Schooljaren.Max(sj => sj.JaarId);

            var fases = _unitOfWork.Context.Fases
                            .Include(f => f.Modules)
                            .Include(f => f.Modules.Select(m => m.StudiePunten))
                            .Include(f => f.Modules.Select(m => m.StudieBelastingen))
                            .Include(f => f.Modules.Select(m => m.ModuleWerkvormen))
                            .Select(f => new
                            {
                                Fase = f,
                                Blokken = f.Modules
                                           .Where(m => m.Schooljaar == maxSchooljaar && m.Status == "Compleet")
                                           .GroupBy(m => m.Blok)
                                           .OrderBy(group => group.Key)
                            })
                            .Where(f => f.Blokken.Any())
                            .OrderBy(f => f.Fase.FaseType)
                            .ThenBy(f => f.Fase.Naam);

            var vm = new StudiegidsViewModel()
            {
                Opleidingsfasen = fases.Select(faseItem => new LessenTabelViewModel()
                {
                    FaseType = faseItem.Fase.FaseType,
                    FaseNaam = faseItem.Fase.Naam,
                    Tabellen = faseItem.Blokken.Select(modulesByBlok => new LesTabelViewModel()
                    {
                        Blok = modulesByBlok.Key,
                        Onderdelen = modulesByBlok.GroupBy(m => m.OnderdeelCode).Select(modulesInBlokByOnderdeel => new OnderdeelTabelViewModel()
                        {
                            Onderdeel = modulesInBlokByOnderdeel.Key,
                            Modules = modulesInBlokByOnderdeel.Select(module => new ModuleTabelViewModel()
                            {
                                Contacturen = module.StudieBelastingen.Where(sb => sb.ContactUren > 0).Select(sb => sb.SBU).ToList(),
                                Cursuscode = module.CursusCode,
                                Omschrijving = module.Naam,
                                Studiepunten = module.StudiePunten.ToList(),
                                Werkvormen = module.ModuleWerkvormen.Select(vw => vw.WerkvormType).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return View(vm);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet, Route("StudieGids/Export/Competenties")]
        public FileStreamResult GetCompetentiesExport()
        {
            CompetentieExportArguments args = new CompetentieExportArguments() { ExportAll = true };
            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll();

            var maxSchooljaar = _unitOfWork.GetRepository<Schooljaar>().GetAll().Max(src => src.JaarId);

            IExportablePack<Competentie> pack = new CompetentieExportablePack(args, competenties);
            Stream fStream = _competentieExporterService.ExportAllAsStream(pack);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Competenties.pdf");

            return new FileStreamResult(fStream, "application/pdf");
        }

        [HttpGet, Route("StudieGids/Export/Leerlijnen")]
        public FileStreamResult GetLeerlijnenExport()
        {
            LeerlijnExportArguments args = new LeerlijnExportArguments() { ExportAll = true };
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll();

            IExportablePack<Leerlijn> pack = new LeerlijnExportablePack(args, leerlijnen);
            Stream fStream = _leerlijnExporterService.ExportAllAsStream(pack);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Leerlijnen.pdf");

            return new FileStreamResult(fStream, "application/pdf");
        }

        [HttpGet, Route("StudieGids/Export/LessenTabel")]
        public FileStreamResult GetLessentabelExport()
        {
            LessenTabelExportArguments args = new LessenTabelExportArguments() { ExportAll = true };
            var data = _unitOfWork.GetRepository<FaseType>().GetAll();

            IExportablePack<FaseType> pack = new LessenTabelExportablePack(args, data);
            Stream fStream = _lessenTabelExporterService.ExportAllAsStream(pack);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=LessenTabel.pdf");

            return new FileStreamResult(fStream, "application/pdf");
        }
    }
}
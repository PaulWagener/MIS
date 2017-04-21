using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Web.Controllers.Api.Interfaces;
using ModuleManager.Web.ViewModels.PartialViewModel;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Web.ViewModels.RequestViewModels;
using AutoMapper;
using ModuleManager.Web.ViewModels.EntityViewModel.Competenties;
using System.Data.Entity;

namespace ModuleManager.Web.Controllers.Api
{
    public class ModuleController : ApiController, IModuleApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterSorterService<Module> _filterSorterService;


        public ModuleController(IFilterSorterService<Module> filterSorterService, IUnitOfWork unitOfWork)
        {
            _filterSorterService = filterSorterService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet, Route("api/Module/Competenties")]
        public IEnumerable<KwaliteitskenmerkViewModel> GetKwaliteitskenmerken()
        {
            return _unitOfWork.Context.Kwaliteitskenmerken
                        .Include(kk => kk.CompetentieOnderdeel)
                        .Include(kk => kk.CompetentieOnderdeel.Competentie)
                        .OrderBy(kk => kk.CompetentieOnderdeel.Competentie.Volgnummer)
                        .ThenBy(kk => kk.CompetentieOnderdeel.Volgnummer)
                        .ThenBy(kk => kk.Volgnummer)
                        .ToList()
                        .Select(kk => new KwaliteitskenmerkViewModel()
                        {
                            Omschrijving = $"{kk.CompetentieOnderdeel.Competentie.Naam}/{kk.CompetentieOnderdeel.Naam}: {kk.Omschrijving}",
                            Id = kk.Id
                        }).ToList();
        }

        [HttpPost, Route("api/Module/GetOverview")]
        public ModuleListViewModel GetOverview(ArgumentsViewModel value)
        {
            ModuleFilterSorterArguments arguments = value != null ? value.ToModuleFilterSorterArguments() : new ModuleFilterSorterArguments();

            if (!User.Identity.IsAuthenticated)
            {
                arguments.StatusFilter = "Compleet (gecontroleerd)";
            }

            int totalRecordCount;
            var modules = _filterSorterService.ProcessData(new ModuleQueryablePack(arguments, _unitOfWork.Context.Modules), out totalRecordCount);

            return new ModuleListViewModel(totalRecordCount, modules);
        }

        [HttpGet, Route("api/Module/Get/{schooljaar}/{key}")]
        public Module GetOne(int schooljaar, string key)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(m => m.CursusCode == key && m.Schooljaar == schooljaar);
            return module;
        }

        [HttpPost, Route("api/Module/Delete")]
        public void Delete(Module entity)
        {
            _unitOfWork.GetRepository<Module>().Delete(entity);
        }

        [HttpPost, Route("api/Module/Create")]
        public void Create(Module entity)
        {
            _unitOfWork.GetRepository<Module>().Create(entity);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}

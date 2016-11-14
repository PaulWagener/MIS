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



        [HttpPost, Route("api/Module/GetOverview")]
        public ModuleListViewModel GetOverview([FromBody] ArgumentsViewModel value)
        {
            var modules = _unitOfWork.GetRepository<Module>().GetAll();

            if (value != null)
            {
                ICollection<string> competentieFilters = null;
                if (value.Filter.Competenties.First() != null)
                    competentieFilters = value.Filter.Competenties;

                ICollection<string> tagFilters = null;
                if (value.Filter.Tags.First() != null)
                    tagFilters = value.Filter.Tags;

                ICollection<string> leerlijnFilters = null;
                if (value.Filter.Leerlijnen.First() != null)
                    leerlijnFilters = value.Filter.Leerlijnen;

                ICollection<string> faseFilters = null;
                if (value.Filter.Fases.First() != null)
                    faseFilters = value.Filter.Fases;

                ICollection<string> blokFilters = null;
                if (value.Filter.Blokken.First() != null)
                    blokFilters = value.Filter.Blokken.ToArray();

                string zoektermFilter = null;
                if (value.Filter.Zoekterm != null)
                    zoektermFilter = value.Filter.Zoekterm;

                int? leerjaarFilter = null;
                if (value.Filter.Leerjaar.HasValue)
                    leerjaarFilter = value.Filter.Leerjaar;

                int column = value.OrderBy.Column;
                string columnName;
                switch (column)
                {
                    case 1: columnName = "Naam"; break;
                    case 2:
                        columnName = "CursusCode";
                        break;
                    case 3:
                        columnName = "Schooljaar";
                        break;
                    case 7:
                        columnName = "Verantwoordelijke";
                        break;
                    default:
                        columnName = "Naam";
                        break;
                }

                bool dir;
                if (value.OrderBy.Dir == "desc")
                {
                    dir = true;
                }
                else
                {
                    dir = false;
                }

                var arguments = new ModuleFilterSorterArguments
                {
                    CompetentieFilters = competentieFilters,
                    TagFilters = tagFilters,
                    LeerlijnFilters = leerlijnFilters,
                    FaseFilters = faseFilters,
                    BlokFilters = blokFilters,
                    ZoektermFilter = zoektermFilter,
                    LeerjaarFilter = leerjaarFilter,
                    SortBy = columnName,
                    SortDesc = dir
                };

                var queryPack = new ModuleQueryablePack(arguments, modules.AsQueryable());
                modules = _filterSorterService.ProcessData(queryPack);

            }

            var enumerable = modules as Module[] ?? modules.ToArray();
            var modArray = enumerable.ToArray().Where(m => m.Status.Equals("Compleet (gecontroleerd)"));
            if (User.Identity.IsAuthenticated)
            {
                modArray = enumerable.ToArray();
            }
            var moduleList = modArray as Module[] ?? modArray.ToArray();
            var moduleListVm = new ModuleListViewModel(moduleList.Count());
            moduleListVm.AddModules(moduleList);
            

            return moduleListVm;
        }

        [HttpGet, Route("api/Module/Get/{schooljaar}/{key}")]
        public Module GetOne(string schooljaar, string key)
        {
            var module = _unitOfWork.GetRepository<Module>().GetOne(new object[] { key, schooljaar });
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

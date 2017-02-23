using ModuleManager.Domain;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class ModuleListViewModel
    {
        public ICollection<ModulePartialViewModel> data { get; set; }
        public int recordsFiltered { get; private set; }
        public int recordsTotal { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recordsTotal">Totaal aantal modules in de datasource"</param>
        public ModuleListViewModel(int recordsTotal)
        {
            this.recordsFiltered = recordsTotal;
            this.recordsTotal = recordsTotal;
        }

        public ModuleListViewModel(int recordsTotal, IEnumerable<Module> modules)
            :this(recordsTotal)
        {
            AddModules(modules);
        }

        public void AddModules(IEnumerable<Module> moduleList)
        {
            data = moduleList
                .Select(Mapper.Map<Module, ModulePartialViewModel>)
                .ToList();
        }
    }
}
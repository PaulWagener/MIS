using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Services
{
    public class ModuleFilterSorterService : IFilterSorterService<Module>
    {
        ModuleFilterService filter;
        ModuleSorterService sorter;

        public ModuleFilterSorterService() 
        {
            filter = new ModuleFilterService();
            sorter = new ModuleSorterService();
        }

        public IEnumerable<Module> ProcessData(IQueryablePack<Module> inputData)
        {
            var filtered = filter.Filter(inputData);
            ModuleQueryablePack toSort = new ModuleQueryablePack(inputData.Args, filtered.AsQueryable());
            var sorted = sorter.Sort(toSort);
            return sorted;
        }
    }
}

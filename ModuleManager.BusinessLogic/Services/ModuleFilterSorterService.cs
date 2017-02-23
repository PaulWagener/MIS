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

        public IQueryable<Module> ProcessData(IQueryablePack<Module> inputData, out int totalRecordCount)
        {
            var filtered = filter.Filter(inputData);
            ModuleQueryablePack toSort = new ModuleQueryablePack(inputData.Args, filtered.AsQueryable());
            var sorted = sorter.Sort(toSort);

            totalRecordCount = sorted.Count();

            if (inputData.Args.Offset.HasValue)
            {
                sorted = sorted.Skip(inputData.Args.Offset.Value);
            }
            if (inputData.Args.Limit.HasValue)
            {
                sorted = sorted.Take(inputData.Args.Limit.Value);
            }

            return sorted;
        }
    }
}

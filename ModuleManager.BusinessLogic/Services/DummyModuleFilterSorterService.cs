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
    public class DummyModuleFilterSorterService : IFilterSorterService<Module>
    {
        public IQueryable<Module> ProcessData(IQueryablePack<Module> inputData, out int totalRecordsCount)
        {
            totalRecordsCount = inputData.Data.Count();
            return inputData.Data;
        }
    }
}

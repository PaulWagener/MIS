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
        public IEnumerable<Module> ProcessData(IQueryablePack<Module> inputData)
        {
            return inputData.Data as IEnumerable<Module>;
        }
    }
}

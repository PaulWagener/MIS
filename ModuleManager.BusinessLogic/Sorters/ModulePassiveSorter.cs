using ModuleManager.BusinessLogic.Interfaces.Sorters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Sorters
{
    /// <summary>
    /// The base decorator, does nothing except return.
    /// </summary>
    public class ModulePassiveSorter : ISorter<Module>
    {
        /// <summary>
        /// Passively returns any data it gets.
        /// </summary>
        /// <param name="toSort">The input data</param>
        /// <param name="args">Apliccable arguments</param>
        /// <returns>The returned data</returns>
        public IQueryable<Module> Sort(IQueryable<Module> toSort, Data.ModuleFilterSorterArguments args)
        {
            return toSort;
        }
    }
}

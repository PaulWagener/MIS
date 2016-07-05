using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Filters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Filters
{
    /// <summary>
    /// The base decorator, does nothing except return.
    /// </summary>
    public class ModulePassiveFilter : IFilter<Module>
    {
        /// <summary>
        /// Passively returns any data it gets.
        /// </summary>
        /// <param name="toQuery">The input data</param>
        /// <param name="args">Apliccable arguments</param>
        /// <returns>The returned data</returns>
        public IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args) 
        {
            return toQuery;
        }
    }
}

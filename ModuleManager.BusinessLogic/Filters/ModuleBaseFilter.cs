using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces;
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
    /// Module Base Filter. This is Extended by all Module Filters, Except the passive.
    /// </summary>
    public abstract class ModuleBaseFilter : IFilter<Module>
    {
        IFilter<Module> parent;
        /// <summary>
        /// Constructor to make the decorator pattern
        /// </summary>
        /// <param name="parent">the previous class in the stack</param>
        public ModuleBaseFilter(IFilter<Module> parent) 
        {
            this.parent = parent;
        }

        /// <summary>
        /// The base function that will filter per argument
        /// </summary>
        /// <param name="toQuery">The data that will be queried</param>
        /// <param name="args">The applied arguments</param>
        /// <returns>The queried data</returns>
        public virtual IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            return parent.Filter(toQuery, args);
        }
    }
}

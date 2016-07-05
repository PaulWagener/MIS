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
    /// Module base Sorter. This is extended by all Module Sorters, except the passive.
    /// </summary>
    public abstract class ModuleBaseSorter : ISorter<Module>
    {
        ISorter<Module> parent;
        /// <summary>
        /// Constructor for making a decorator pattern
        /// </summary>
        /// <param name="parent">the previous class in the stack</param>
        public ModuleBaseSorter(ISorter<Module> parent) 
        {
            this.parent = parent;
        }

        /// <summary>
        /// The base function the will sort per argument.
        /// </summary>
        /// <param name="toSort">The data that will be sorted</param>
        /// <param name="args">The applied arguments</param>
        /// <returns>The sorted data</returns>
        public virtual IQueryable<Module> Sort(IQueryable<Module> toSort, Data.ModuleFilterSorterArguments args)
        {
            return parent.Sort(toSort, args);
        }
    }
}

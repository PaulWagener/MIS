using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.BusinessLogic.Interfaces.Sorters;
using ModuleManager.BusinessLogic.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Services
{
    /// <summary>
    /// Sorting class for use with Modules
    /// </summary>
    public class ModuleSorterService : ISorterService<Domain.Module>
    {
        ISorter<Domain.Module> moduleSorterStrategy;

        /// <summary>
        /// Constructor: Builds the contained Strategy
        /// </summary>
        public ModuleSorterService() 
        {
            moduleSorterStrategy = new ModulePassiveSorter();

            //Build Reflection Here
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "ModuleManager.BusinessLogic.Sorters.ModuleSorterStack" && !t.IsDefined(typeof(CompilerGeneratedAttribute), false)
                        select t;
            Type[] typeArgs = { typeof(ISorter<Domain.Module>) };
            foreach (Type t in types) 
            {
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null) 
                {
                    object[] parameters = { moduleSorterStrategy };
                    moduleSorterStrategy = ctor.Invoke(parameters) as ISorter<Domain.Module>;
                }
            }
        }

        /// <summary>
        /// Operates the sorting on the input List of Modules
        /// </summary>
        /// <param name="qPack">Pack with Data and required Arguments</param>
        /// <returns>List of Sorted Modules</returns>
        public IQueryable<Domain.Module> Sort(Interfaces.IQueryablePack<Domain.Module> qPack)
        {
            if (qPack.Args.SortBy != null)
            {
                return moduleSorterStrategy.Sort(qPack.Data, qPack.Args);
            }
            return qPack.Data;
        }
    }
}

using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Filters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Filters.ModuleFilterStack
{
    /// <summary>
    /// Filters Modules based on the related "Blok"
    /// </summary>
    public class ModuleBlokFilter : ModuleBaseFilter
    {
        public ModuleBlokFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.BlokFilters != null && args.BlokFilters.Count > 0)
            {
                toQuery = toQuery.Where(module => args.BlokFilters.Contains(module.Blok));
            }

            return base.Filter(toQuery, args);
        }
    }
}

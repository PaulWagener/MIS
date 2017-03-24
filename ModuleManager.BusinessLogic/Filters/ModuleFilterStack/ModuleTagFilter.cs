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
    public class ModuleTagFilter : ModuleBaseFilter
    {
        public ModuleTagFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.TagFilters != null && args.TagFilters.Count > 0)
            {
                foreach (var tag in args.TagFilters)
                {
                    var tmp = tag; // because of capture problem
                    toQuery = toQuery.Where(module => module.Tags.Any(t => t.Naam == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

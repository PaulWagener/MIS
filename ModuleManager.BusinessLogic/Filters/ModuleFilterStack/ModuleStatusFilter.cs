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
    public class ModuleStatusFilter : ModuleBaseFilter
    {
        public ModuleStatusFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.StatusFilter != null && args.StatusFilter.Length > 0)
            {
                foreach (var status in args.StatusFilter)
                {
                    var tmp = status; // because of capture problem
                    toQuery = toQuery.Where(module => module.Status.Any(s => s == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

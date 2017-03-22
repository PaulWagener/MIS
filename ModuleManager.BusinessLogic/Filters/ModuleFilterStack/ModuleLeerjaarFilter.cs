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
    public class ModuleLeerjaarFilter : ModuleBaseFilter
    {
        public ModuleLeerjaarFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.LeerjaarFilter.HasValue)
            {
                toQuery = toQuery.Where(m => m.Schooljaar == args.LeerjaarFilter);
            }

            return base.Filter(toQuery, args);
        }
    }
}

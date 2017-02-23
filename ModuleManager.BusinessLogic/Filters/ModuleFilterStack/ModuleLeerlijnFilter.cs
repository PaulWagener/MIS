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
    public class ModuleLeerlijnFilter : ModuleBaseFilter
    {
        public ModuleLeerlijnFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.LeerlijnFilters != null && args.LeerlijnFilters.Count > 0)
            {
                foreach (var leerlijn in args.LeerlijnFilters)
                {
                    var tmp = leerlijn; // because of capture problem
                    toQuery = toQuery.Where(module => module.Leerlijnen.Any(ll => ll.Naam == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

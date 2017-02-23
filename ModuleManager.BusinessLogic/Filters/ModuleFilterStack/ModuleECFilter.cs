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
    public class ModuleECFilter : ModuleBaseFilter
    {
        public ModuleECFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.ECfilters != null && args.ECfilters.Count > 0)
            {
                foreach (var ec in args.ECfilters)
                {
                    var tmp = ec; // because of capture problem
                    toQuery = toQuery.Where(module => module.StudiePunten.Any(sp => sp.EC == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

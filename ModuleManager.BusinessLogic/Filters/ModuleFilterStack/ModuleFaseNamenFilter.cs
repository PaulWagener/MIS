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
    public class ModuleFaseNamenFilter : ModuleBaseFilter
    {
        public ModuleFaseNamenFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.FaseFilters != null && args.FaseFilters.Count > 0)
            {
                foreach (var fase in args.FaseFilters)
                {
                    var tmp = fase; // because of capture problem
                    toQuery = toQuery.Where(module => module.Fases.Any(f => f.Naam == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

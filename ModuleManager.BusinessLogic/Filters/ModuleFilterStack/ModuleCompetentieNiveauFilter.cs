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
    public class ModuleCompetentieNiveauFilter : ModuleBaseFilter
    {
        public ModuleCompetentieNiveauFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.CompetentieNiveauFilters != null && args.CompetentieNiveauFilters.Count > 0)
            {
                foreach (var niveau in args.CompetentieNiveauFilters)
                {
                    var tmp = niveau; // because of capture problem
                    toQuery = toQuery.Where(module => module.ModuleCompetenties.Any(mc => mc.Niveau == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

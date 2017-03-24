using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Filters;
using ModuleManager.Domain;
using System.Linq;

namespace ModuleManager.BusinessLogic.Filters.ModuleFilterStack
{
    public class ModuleCompetentieFilter : ModuleBaseFilter
    {
        public ModuleCompetentieFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.CompetentieFilters != null && args.CompetentieFilters.Count > 0)
            {
                foreach (var competentie in args.CompetentieFilters)
                {
                    var tmp = competentie; // because of capture problem
                    toQuery = toQuery.Where(module => module.ModuleCompetenties.Any(mc => mc.Competentie.Naam == tmp));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

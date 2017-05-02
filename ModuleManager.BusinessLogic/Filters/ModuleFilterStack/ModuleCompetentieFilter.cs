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
            if (args.KwaliteitskenmerkIds != null && args.KwaliteitskenmerkIds.Count > 0)
            {
                foreach (var kkId in args.KwaliteitskenmerkIds)
                {
                    var tmp = kkId; // because of capture problem
                    toQuery = toQuery.Where(module => module.Leerdoelen.Any(ld => ld.Kwaliteitskenmerken.Any(kk => kk.Id == tmp)));
                }
            }

            return base.Filter(toQuery, args);
        }
    }
}

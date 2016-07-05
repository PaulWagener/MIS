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
                List<Module> result = new List<Module>();
                foreach (string arg in args.CompetentieNiveauFilters)
                {
                    var selectedModule = 
                        from m in toQuery
                            where
                                m.ModuleCompetenties.Any(
                                element => (element.Niveau ?? "").ToLower().Contains((arg ?? "").ToLower())
                                )
                        select m;

                    result.AddRange(selectedModule.Where(x => !result.Contains(x)));
                }

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

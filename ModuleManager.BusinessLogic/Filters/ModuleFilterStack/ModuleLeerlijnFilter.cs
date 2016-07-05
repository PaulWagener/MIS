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
                List<Module> result = new List<Module>();
                foreach (string arg in args.LeerlijnFilters)
                {
                    var selectedModule = 
                        from m in toQuery
                            where
                                m.Leerlijnen.Any(
                                element => (element.Naam ?? "").ToLower().Contains((arg ?? "").ToLower())
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

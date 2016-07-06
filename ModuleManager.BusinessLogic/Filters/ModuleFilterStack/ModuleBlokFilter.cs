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
    /// <summary>
    /// Filters Modules based on the related "Blok"
    /// </summary>
    public class ModuleBlokFilter : ModuleBaseFilter
    {
        public ModuleBlokFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.BlokFilters != null && args.BlokFilters.Count > 0)
            {
                List<Module> result = new List<Module>();
                foreach (string arg in args.BlokFilters)
                {
                    var selectedModule = 
                        from m in toQuery
                            where
                                m.FaseModules.Any()
                        select m;

                    result.AddRange(selectedModule.Where(x => !result.Contains(x)));
                }

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

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
    public class ModuleStatusFilter : ModuleBaseFilter
    {
        public ModuleStatusFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.StatusFilter != null && args.StatusFilter.Length > 0)
            {
                List<Module> result = new List<Module>();

                var selectedModule = 
                    from m in toQuery
                        where
                            (m.Status ?? "").ToLower().Contains((args.StatusFilter ?? "").ToLower())
                    select m;

                result.AddRange(selectedModule.Where(x => !result.Contains(x)));

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

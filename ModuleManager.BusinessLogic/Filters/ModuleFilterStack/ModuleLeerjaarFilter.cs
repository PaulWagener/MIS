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
    public class ModuleLeerjaarFilter : ModuleBaseFilter
    {
        public ModuleLeerjaarFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.LeerjaarFilter != null && args.LeerjaarFilter.Length > 0)
            {
                List<Module> result = new List<Module>();
                    
                var selectedModule = 
                    from m in toQuery
                        where
                            (m.Schooljaar ?? "").ToLower().Contains(args.LeerjaarFilter.ToLower())
                    select m;
                result.AddRange(selectedModule.Where(x => !result.Contains(x)));

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

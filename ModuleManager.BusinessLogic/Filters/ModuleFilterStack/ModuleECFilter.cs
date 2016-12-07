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
                List<Module> result = new List<Module>();
                foreach (int arg in args.ECfilters)
                {
                    var selectedModule = 
                        from m in toQuery
                            where
                                m.StudiePunten.Select(element => element.EC).Contains(arg)
                        select m;

                    result.AddRange(selectedModule.Where(x => !result.Contains(x)));
                }

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

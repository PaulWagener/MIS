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
            if (args.LeerjaarFilter.HasValue)
            {
                List<Module> result = new List<Module>();

                var selectedModule = toQuery.Where(m => m.Schooljaar == args.LeerjaarFilter);

                result.AddRange(selectedModule.Where(x => !result.Contains(x)));

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}

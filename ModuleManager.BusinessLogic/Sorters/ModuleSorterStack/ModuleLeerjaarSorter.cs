using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Sorters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Sorters.ModuleSorterStack
{
    public class ModuleLeerjaarSorter : ModuleBaseSorter
    {
        public ModuleLeerjaarSorter(ISorter<Module> parent) : base(parent) { }

        public override IQueryable<Module> Sort(IQueryable<Module> toSort, ModuleFilterSorterArguments args) 
        {
            if (args.SortBy.Equals("Schooljaar")) 
            {
                if (args.SortDesc) 
                {
                    toSort = toSort.OrderByDescending(element => (element.Schooljaar ?? ""));
                }
                else 
                {
                    toSort = toSort.OrderBy(element => (element.Schooljaar ?? "")); 
                }
            }

            return base.Sort(toSort, args);
        }
    }
}

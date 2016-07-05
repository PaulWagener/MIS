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
    public class ModuleCursusCodeSorter : ModuleBaseSorter
    {
        public ModuleCursusCodeSorter(ISorter<Module> parent) : base(parent) { }

        public override IQueryable<Module> Sort(IQueryable<Module> toSort, ModuleFilterSorterArguments args) 
        {
            if (args.SortBy.Equals("CursusCode")) 
            {
                if (args.SortDesc) 
                {
                    toSort = toSort.OrderByDescending(element => (element.CursusCode ?? ""));
                }
                else 
                {
                    toSort = toSort.OrderBy(element => (element.CursusCode ?? "")); 
                }
            }

            return base.Sort(toSort, args);
        }
    }
}

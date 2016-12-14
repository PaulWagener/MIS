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
    public class ModuleGenericZoektermFilter : ModuleBaseFilter
    {
        public ModuleGenericZoektermFilter(IFilter<Module> parent) : base(parent) { }
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, ModuleFilterSorterArguments args)
        {
            if (args.ZoektermFilter != null && args.ZoektermFilter.Length > 0) 
            {
                toQuery = from m in toQuery where 
                              (
                              (m.Beschrijving ?? "").Contains(args.ZoektermFilter.ToLower()) ||
                              (m.CursusCode ?? "").ToLower().Contains(args.ZoektermFilter.ToLower()) ||
                              (from d in m.Docenten select (d.Naam ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (from l in m.Leerdoelen select (l.Beschrijving ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (from l in m.Leerdoelen select (l.CursusCode ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (from l in m.Leerlijnen select (l.Naam ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (from lm in m.Leermiddelen select (lm.Beschrijving ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (from lm in m.Leermiddelen select (lm.CursusCode ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (m.Naam ?? "").ToLower().Contains(args.ZoektermFilter.ToLower()) ||
                              (from t in m.Tags select (t.Naam ?? "").ToLower()).Contains(args.ZoektermFilter.ToLower()) ||
                              (m.Verantwoordelijke.Naam ?? "").ToLower().Contains(args.ZoektermFilter.ToLower()) 
                              )
                          select m; 
            }

            return base.Filter(toQuery, args);
        }
    }
}

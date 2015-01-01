﻿using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Filters;
using ModuleManager.DomainDAL;
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
        public override IQueryable<Module> Filter(IQueryable<Module> toQuery, Arguments args)
        {
            if (args.Status1 != null)
            {
                List<Module> result = new List<Module>();

                var selectedModule = 
                    from m in toQuery
                        where
                            m.Status.ToLower().Contains(args.Status1.ToLower())
                    select m;

                result.AddRange(selectedModule);

                toQuery = result.AsQueryable();
            }

            return base.Filter(toQuery, args);
        }
    }
}
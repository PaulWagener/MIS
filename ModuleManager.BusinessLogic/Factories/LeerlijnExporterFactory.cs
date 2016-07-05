using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Exporters;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Factories
{
    /// <summary>
    /// Builds exporter patterns based on option object
    /// </summary>
    public class LeerlijnExporterFactory
    {
        Dictionary<string, Type> usableTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Constructor call, builds pre-stack dictionairy for use in deco pattern
        /// </summary>
        public LeerlijnExporterFactory()
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "ModuleManager.BusinessLogic.Exporters.LeerlijnExporterStack" && !t.IsDefined(typeof(CompilerGeneratedAttribute), false)
                        select t;

            foreach (Type t in types)
            {
                usableTypes.Add(t.Name, t);
            }
        }

        /// <summary>
        /// Get a decorator pattern based on given options
        /// </summary>
        /// <param name="opt">Pre-defined options</param>
        /// <returns>Decorator pattern for exporting</returns>
        public IExporter<Domain.Leerlijn> GetStrategy(LeerlijnExportArguments opt) 
        {
            //make sure you keep the ExportOptions in Sync with the Stack. That way, you can just use ifs here.
            IExporter<Domain.Leerlijn> strategy = new LeerlijnPassiveExporter();

            opt.ExportNaam = true; //Always Needed, table of contents is filled with this

            Type[] typeArgs = { typeof(IExporter<Domain.Leerlijn>) };

            
            if (opt.ExportAll || opt.ExportNaam)
            {
                Type t = usableTypes["LeerlijnNaamExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Leerlijn>;
                }
            }

            if (opt.ExportAll || opt.ExportModules)
            {
                Type t = usableTypes["LeerlijnModulesExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Leerlijn>;
                }
            }

            if (opt.ExportAll || opt.ExportCompetenties)
            {
                Type t = usableTypes["LeerlijnCompetentiesExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Leerlijn>;
                }
            }

            //This won't happen. But hey, risky things an' all.
            if (strategy == null)
            {
                strategy = new LeerlijnPassiveExporter();
            }

            return strategy;
        }
    }
}

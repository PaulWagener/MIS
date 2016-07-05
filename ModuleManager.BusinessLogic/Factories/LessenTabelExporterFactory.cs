using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Exporters;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
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
    public class LessenTabelExporterFactory
    {
        Dictionary<string, Type> usableTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Constructor call, builds pre-stack dictionairy for use in deco pattern
        /// </summary>
        public LessenTabelExporterFactory()
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "ModuleManager.BusinessLogic.Exporters.LessenTabelExporterStack" && !t.IsDefined(typeof(CompilerGeneratedAttribute), false)
                        select t;

            foreach (Type t in types)
            {
                usableTypes.Add(t.Name, t);
            }
        }

        public IExporter<FaseType> GetStrategy(LessenTabelExportArguments opt)
        {
            //make sure you keep the ExportOptions in Sync with the Stack. That way, you can just use ifs here.
            IExporter<FaseType> strategy = new LessenTabelPassiveExporter();

            opt.ExportNaam = true; //Always Needed, table of contents is filled with this

            Type[] typeArgs = { typeof(IExporter<FaseType>) };

            if (opt.ExportAll || opt.ExportNaam)
            {
                Type t = usableTypes["LessenTabelNaamExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<FaseType>;
                }
            }

            if (opt.ExportAll || opt.ExportTabellen) 
            {
                Type t = usableTypes["LessenTabelInhoudExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<FaseType>;
                }
            }

            //This won't happen. But hey, risky things an' all.
            if (strategy == null)
            {
                strategy = new LessenTabelPassiveExporter();
            }

            return strategy;
        }
    }
}

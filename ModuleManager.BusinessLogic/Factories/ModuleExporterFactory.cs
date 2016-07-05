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
    public class ModuleExporterFactory
    {
        Dictionary<string, Type> usableTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Constructor call, builds pre-stack dictionairy for use in deco pattern
        /// </summary>
        public ModuleExporterFactory()
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "ModuleManager.BusinessLogic.Exporters.ModuleExporterStack" && !t.IsDefined(typeof(CompilerGeneratedAttribute), false)
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
        public IExporter<Domain.Module> GetStrategy(ModuleExportArguments opt) 
        {
            //make sure you keep the ExportOptions in Sync with the Stack. That way, you can just use ifs here.
            IExporter<Domain.Module> strategy = new ModulePassiveExporter();

            opt.ExportNaam = true; //Always Needed, table of contents is filled with this

            Type[] typeArgs = { typeof(IExporter<Domain.Module>) };

            if (opt.ExportAll || opt.ExportNaam)
            {
                Type t = usableTypes["ModuleNaamExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportCursusCode)
            {
                Type t = usableTypes["ModuleCursusCodeExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null) 
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportBeschrijving)
            {
                Type t = usableTypes["ModuleBeschrijvingExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportAlgInfo)
            {
                Type t = usableTypes["ModuleAlgemeneInfoExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportStudieBelasting)
            {
                Type t = usableTypes["ModuleStudieBelastingExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportOrganisatie)
            {
                Type t = usableTypes["ModuleOrganisatieExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportWeekplanning)
            {
                Type t = usableTypes["ModuleWeekPlanningExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportBeoordeling)
            {
                Type t = usableTypes["ModuleBeoordelingExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportLeermiddelen)
            {
                Type t = usableTypes["ModuleLeermiddelenExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportLeerdoelen)
            {
                Type t = usableTypes["ModuleLeerdoelenExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportCompetenties)
            {
                Type t = usableTypes["ModuleCompetentieExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportLeerlijnen)
            {
                Type t = usableTypes["ModuleLeerlijnenExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            if (opt.ExportAll || opt.ExportTags)
            {
                Type t = usableTypes["ModuleTagExporter"];
                var ctor = t.GetConstructor(typeArgs);
                if (ctor != null)
                {
                    object[] parameters = { strategy };
                    strategy = ctor.Invoke(parameters) as IExporter<Domain.Module>;
                }
            }

            //This won't happen. But hey, risky things an' all.
            if (strategy == null)
            {
                strategy = new ModulePassiveExporter();
            }

            return strategy;
        }
    }
}

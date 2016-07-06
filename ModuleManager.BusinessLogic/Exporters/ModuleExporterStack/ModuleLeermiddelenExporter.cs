using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.ModuleExporterStack
{
    public class ModuleLeermiddelenExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleLeermiddelenExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export educational tools to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("Leermiddelen", "Heading2");
            p.AddLineBreak();

            p = sect.AddParagraph();
            foreach(Leermiddel lm in toExport.Leermiddelen)
            {
                p.AddText(" - " + (lm.Beschrijving ?? ""));
                p.AddLineBreak();
            }

            p.AddLineBreak();

            return sect;
        }
    }
}

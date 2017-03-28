using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.CompetentieExporterStack
{
    public class CompetentieCodeExporter : CompetentieBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public CompetentieCodeExporter(IExporter<Competentie> parent) : base(parent) { }

        /// <summary>
        /// Export name to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Competentie toExport, Section sect)
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph();
            p.Format.Font.Color = Colors.DarkGray;
            p.AddText("Code: " + (toExport.Naam ?? ""));
            p.AddLineBreak();

            return sect;

            // TODO: Wat doet de code export? Competenties hebben geen code.
        }
    }
}

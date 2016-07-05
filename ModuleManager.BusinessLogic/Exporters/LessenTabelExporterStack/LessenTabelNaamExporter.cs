using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.LessenTabelExporterStack
{
    public class LessenTabelNaamExporter : LessenTabelBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public LessenTabelNaamExporter(IExporter<FaseType> parent) : base(parent) { }

        /// <summary>
        /// Export name to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(FaseType toExport, Section sect)
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph();
            p.AddFormattedText((toExport.Type ?? "Data niet gevonden"), "Heading1");
            p.Format.OutlineLevel = OutlineLevel.Level1;
            p.AddBookmark(toExport.Type ?? "Data niet gevonden");
            p.AddLineBreak();

            return sect;
        }
    }
}

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
    public class CompetentieBeschrijvingExporter : CompetentieBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public CompetentieBeschrijvingExporter(IExporter<Competentie> parent) : base(parent) { }

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
            Paragraph p = sect.AddParagraph("Over deze Competentie", "Heading2");
            p.AddLineBreak();

            p = sect.AddParagraph((toExport.Beschrijving ?? "").Replace("$$", "\n"));
            p.AddLineBreak();

            return sect;
        }
    }
}

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.ModuleExporterStack
{
    public class ModuleStudieBelastingExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleStudieBelastingExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export weight in ECs to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("StudieBelasting", "Heading2");
            p.AddLineBreak();

            Table table = sect.AddTable();
            Column actCol = table.AddColumn();
            Column timeCol = table.AddColumn(Unit.FromCentimeter(3));
            Column freqCol = table.AddColumn(Unit.FromCentimeter(3));
            Column sbuCol = table.AddColumn(Unit.FromCentimeter(1.5));
            actCol.Width = sect.Document.DefaultPageSetup.PageWidth - sect.Document.DefaultPageSetup.RightMargin - sect.Document.DefaultPageSetup.LeftMargin - timeCol.Width - freqCol.Width - sbuCol.Width;
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.DarkGray;

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Activiteit").Format.Font.Bold = true;
            row.Cells[1].AddParagraph("Duur").Format.Font.Bold = true;
            row.Cells[2].AddParagraph("Frequentie").Format.Font.Bold = true;
            row.Cells[3].AddParagraph("SBU").Format.Font.Bold = true;

            foreach (StudieBelasting sb in toExport.StudieBelastingen) 
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph(sb.Activiteit ?? "");
                row.Cells[1].AddParagraph(sb.Duur ?? "");
                row.Cells[2].AddParagraph(sb.Frequentie ?? "");
                row.Cells[3].AddParagraph(sb.SBU.ToString());
            }

            row = table.AddRow();
            row.Cells[2].AddParagraph("Totaal").Format.Font.Bold = true; ;
            row.Cells[3].AddParagraph(toExport.StudieBelastingen.Sum(x => x.SBU).ToString());

            p = sect.AddParagraph();
            p.AddLineBreak();

            return sect;
        }
    }
}

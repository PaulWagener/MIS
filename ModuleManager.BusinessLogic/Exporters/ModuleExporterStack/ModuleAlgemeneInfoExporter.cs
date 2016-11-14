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
    public class ModuleAlgemeneInfoExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleAlgemeneInfoExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export generic information to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph();
            p.AddLineBreak();
            p.AddText("Algemene Informatie");
            p.Style = "Heading2";

            Table table = sect.AddTable();
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.DarkGray;

            Column keyCol = table.AddColumn(Unit.FromCentimeter(3));
            //Second Column will spread over the remainder of the page.
            Column infoCol = table.AddColumn();
            infoCol.Width = sect.Document.DefaultPageSetup.PageWidth - sect.Document.DefaultPageSetup.RightMargin - sect.Document.DefaultPageSetup.LeftMargin - keyCol.Width;

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Schooljaar");
            row.Cells[1].AddParagraph("20" + (toExport.Schooljaar / 100) + "-20" + (toExport.Schooljaar % 100));

            row = table.AddRow();
            row.Cells[0].AddParagraph("Blokken");
            Paragraph pCell = row.Cells[1].AddParagraph();

            bool notFirstIteration = false;

            foreach(Fase fm in toExport.Fases)
            {
                if (notFirstIteration) { pCell.AddText(", "); }
                notFirstIteration = true;
            }

            row = table.AddRow();
            row.Cells[0].AddParagraph("Cursus Code");
            row.Cells[1].AddParagraph(toExport.CursusCode ?? "");

            row = table.AddRow();
            row.Cells[0].AddParagraph("Toetsen");
            foreach (StudiePunt sp in toExport.StudiePunten) 
            {
                row.Cells[1].AddParagraph(" - " + (sp.ToetsCode ?? "") + ": " + sp.EC + " EC");
            }

            row = table.AddRow();
            row.Cells[0].AddParagraph("Werkvormen");
            pCell = row.Cells[1].AddParagraph();
            notFirstIteration = false;
            foreach(ModuleWerkvorm wv in toExport.ModuleWerkvormen)
            {
                if (notFirstIteration) { pCell.AddText("+"); }
                pCell.AddText(wv.WerkvormType ?? "NOT FOUND");
                notFirstIteration = true;
            }

            p = sect.AddParagraph();
            p.AddLineBreak();

            return sect;
        }
    }
}

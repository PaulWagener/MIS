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
    public class ModuleCompetentieExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleCompetentieExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export skills to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("Competenties", "Heading2");
            p.AddLineBreak();

            sect.AddParagraph("Competentie export komt later nog...");

            // TODO: Competenties exporteren goed maken.

            //foreach (ModuleCompetentie cp in toExport.Leerdoelen.SelectMany(ld => ld.Kwaliteitskenmerken) 
            //{
            //    Table table = sect.AddTable();
            //    Column codeCol = table.AddColumn();
            //    Column lvlCol = table.AddColumn(Unit.FromCentimeter(3));
            //    codeCol.Width = sect.Document.DefaultPageSetup.PageWidth - sect.Document.DefaultPageSetup.RightMargin - sect.Document.DefaultPageSetup.LeftMargin - lvlCol.Width;
            //    table.Borders.Width = 0.75;
            //    table.Borders.Color = Colors.DarkGray;

            //    Row row = table.AddRow();
            //    row.Cells[0].AddParagraph("Naam").Format.Font.Bold = true;
            //    row.Cells[1].AddParagraph("Niveau").Format.Font.Bold = true;

            //    row = table.AddRow();
            //    row.Cells[0].AddParagraph(cp.Competentie.Naam ?? "NOT FOUND");
            //    row.Cells[1].AddParagraph(cp.Niveau ?? "NOT FOUND");

            //    row = table.AddRow();
            //    row.Cells[0].AddParagraph("Beschrijving").Format.Font.Bold = true;
            //    row.Cells[0].MergeRight = 1;

            //    row = table.AddRow();
            //    row.Cells[0].AddParagraph((cp.Competentie.Beschrijving ?? "").Replace("$$", "\n"));
            //    row.Cells[0].MergeRight = 1;

            //    p = sect.AddParagraph();
            //    p.AddLineBreak();
            //}

            return sect;
        }
    }
}

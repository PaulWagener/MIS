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
    public class ModuleWeekPlanningExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleWeekPlanningExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export week scheduling to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("Weekplanning", "Heading2");
            p.AddLineBreak();

            Table table = sect.AddTable();
            Column weekCol = table.AddColumn(Unit.FromCentimeter(1.5));
            Column subjectCol = table.AddColumn();
            subjectCol.Width = sect.Document.DefaultPageSetup.PageWidth - sect.Document.DefaultPageSetup.RightMargin - sect.Document.DefaultPageSetup.LeftMargin - weekCol.Width;
            table.Borders.Width = 0.75;
            table.Borders.Color = Colors.DarkGray;

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Week").Format.Font.Bold = true;
            row.Cells[1].AddParagraph("Onderwerpen").Format.Font.Bold = true;

            foreach (Weekplanning wp in toExport.Weekplanningen) 
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph(wp.Week ?? "");
                row.Cells[1].AddParagraph(wp.Onderwerp ?? "");
            }

            p = sect.AddParagraph();
            p.AddLineBreak();

            return sect;
        }
    }
}

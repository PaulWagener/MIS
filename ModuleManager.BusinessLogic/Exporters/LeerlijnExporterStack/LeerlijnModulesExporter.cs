using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.LeerlijnExporterStack
{
    public class LeerlijnModulesExporter : LeerlijnBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public LeerlijnModulesExporter(IExporter<Leerlijn> parent) : base(parent) { }

        /// <summary>
        /// Export name to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Leerlijn toExport, Section sect)
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("Modules in deze leerlijn", "Heading2");
            p.AddLineBreak();

            p = sect.AddParagraph();
            p.AddFormattedText("De leerlijnen zijn klikbaar in PDF viewers").Font.Color = Colors.DarkGray;
            p.AddLineBreak();
            p.AddLineBreak();

            foreach (Module m in toExport.Modules) 
            {
                p.AddFormattedText((m.Naam ?? "Data niet gevonden")).Font.Bold = true;
                p.AddLineBreak();

                p.AddFormattedText("Komt ook voor in:").Font.Color = Colors.DarkGray;
                p.AddLineBreak();

                List<Leerlijn> otherLines = m.Leerlijnen.ToList();
                foreach (Leerlijn l in otherLines) 
                {
                    if(!l.Naam.Equals(toExport.Naam))
                    {
                        p.AddTab();
                        Hyperlink hyperlink = p.AddHyperlink((l.Naam ?? "Data niet gevonden"));
                        hyperlink.AddText((l.Naam ?? "Data niet gevonden" ) + ", zie ook pagina ");
                        hyperlink.AddPageRefField((l.Naam ?? "Data niet gevonden"));
                        hyperlink.Font.Underline = Underline.Single;

                        p.AddLineBreak();
                    }
                }
                p.AddLineBreak();
            }

            return sect;
        }
    }
}

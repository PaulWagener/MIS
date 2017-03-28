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
    public class LeerlijnCompetentiesExporter : LeerlijnBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public LeerlijnCompetentiesExporter(IExporter<Leerlijn> parent) : base(parent) { }

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
            Paragraph p = sect.AddParagraph("Competenties in deze Leerlijn", "Heading2");
            p.AddLineBreak();

            p = sect.AddParagraph();

            List<Kwaliteitskenmerk> allKwaliteitskenmerken = new List<Kwaliteitskenmerk>();

            foreach (Leerdoel leerdoel in toExport.Modules.SelectMany(m => m.Leerdoelen)) 
            {
                foreach (var kenmerk in leerdoel.Kwaliteitskenmerken)
                {
                    if (!allKwaliteitskenmerken.Contains(kenmerk))
                    {
                        allKwaliteitskenmerken.Add(kenmerk);
                    }
                }
            }

            var ordered = allKwaliteitskenmerken.OrderBy(k => k.CompetentieOnderdeel.Competentie.Volgnummer)
                                                .ThenBy(k => k.CompetentieOnderdeel.Volgnummer)
                                                .ThenBy(k => k.Volgnummer);

            foreach (var kenmerk in ordered) 
            {
                // TODO: Een tostring of iets dergelijks in de domain class.
                StringBuilder sb = new StringBuilder();
                sb.Append(kenmerk.CompetentieOnderdeel.Competentie.Naam);
                sb.Append(" - ");
                sb.Append(kenmerk.CompetentieOnderdeel.Competentie.Naam.Substring(0, 1).ToUpper());
                sb.Append(kenmerk.CompetentieOnderdeel.Volgnummer);
                sb.Append(" ");
                sb.Append(kenmerk.CompetentieOnderdeel.Naam);
                sb.Append(" - K");
                sb.Append(kenmerk.Volgnummer);
                sb.Append(" ");
                sb.Append(kenmerk.Omschrijving);
                p.AddText(sb.ToString());
                p.AddLineBreak();
            }
            p.AddLineBreak();

            return sect;
        }
    }
}

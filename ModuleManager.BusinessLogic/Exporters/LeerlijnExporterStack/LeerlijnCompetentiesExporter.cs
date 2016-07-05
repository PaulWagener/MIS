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

            List<Competentie> accumulatedCompetences = new List<Competentie>();

            foreach (Module m in toExport.Modules) 
            {
                accumulatedCompetences = new List<Competentie>();
                foreach(ModuleCompetentie mc in m.ModuleCompetenties)
                {
                    if(!accumulatedCompetences.Contains(mc.Competentie))
                    {
                        accumulatedCompetences.Add(mc.Competentie);
                    }
                }
            }

            foreach (Competentie c in accumulatedCompetences) 
            {
                p.AddText(" - " + (c.Naam ?? "Data incompleet"));
                p.AddLineBreak();
            }
            p.AddLineBreak();

            return sect;
        }
    }
}

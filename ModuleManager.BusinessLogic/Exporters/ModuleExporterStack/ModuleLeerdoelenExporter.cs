﻿using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.ModuleExporterStack
{
    public class ModuleLeerdoelenExporter : ModuleBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public ModuleLeerdoelenExporter(IExporter<Module> parent) : base(parent) { }

        /// <summary>
        /// Export educational goals to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(Module toExport, Section sect) 
        {
            base.Export(toExport, sect);

            //custom code
            Paragraph p = sect.AddParagraph("Leerdoelen", "Heading2");
            p = sect.AddParagraph("De student kan...");

            foreach (Leerdoel ld in toExport.Leerdoelen) 
            {
                p = sect.AddParagraph(ld.Beschrijving, "bulletlist");

                foreach (var kk in ld.Kwaliteitskenmerken)
                {
                    p = sect.AddParagraph();
                    p.Style = "bulletlist2";

                    p.AddFormattedText(kk.CompetentieOnderdeel.Competentie.Naam + " / ", TextFormat.Bold);
                    p.AddFormattedText(kk.CompetentieOnderdeel.Naam + ": ", TextFormat.Italic | TextFormat.Bold);
                    p.AddText(kk.Omschrijving);
                }
            }

            return sect;
        }
    }
}

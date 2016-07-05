using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters.LessenTabelExporterStack
{
    public class LessenTabelInhoudExporter : LessenTabelBaseExporter
    {
        /// <summary>
        /// Constructor for stacking decorators
        /// </summary>
        /// <param name="parent">The previous decorator pattern</param>
        public LessenTabelInhoudExporter(IExporter<FaseType> parent) : base(parent) { }

        /// <summary>
        /// Export name to a section in a document
        /// </summary>
        /// <param name="toExport">The data to export from</param>
        /// <param name="sect">The section to write on</param>
        /// <returns>The section with appended data</returns>
        public override Section Export(FaseType toExport, Section sect)
        {
            //base.Export(toExport, sect);

            ////custom code
            //foreach (Fase f in toExport.Fases) 
            //{
            //    Paragraph p = sect.AddParagraph((toExport.Type ?? "Data niet gevonden") + " - " + (f.Naam ?? "Data niet gevonden"), "Heading2");
            //    p.AddLineBreak();
            //    FormattedText text = p.AddFormattedText("Als U hieronder geen tabel ziet, bevat de database nog incomplete Modules.");
            //    text.Font.Color = Colors.DarkGray;
            //    text.Font.Size = 8;

            //    var ungroupedFaseModules = f.FaseType.Where(element => element.Module.Status.Equals("Compleet (gecontroleerd)"));

            //    foreach (var faseModules in ungroupedFaseModules.GroupBy(element => element.Blok)) 
            //    {

            //        var semester =  "" + Math.Ceiling(Convert.ToInt32(faseModules.First().Blok) / 2.0);

            //        p = sect.AddParagraph("Blok " + faseModules.First().Blok + " - " + semester + "e Semester");
            //        p.Format.Font.Bold = true;

            //        Table table = sect.AddTable();
            //        table.Borders.Width = 0.75;
            //        table.Borders.Color = Colors.DarkGray;

            //        Column onderdeelCol = table.AddColumn(Unit.FromCentimeter(2));
            //        Column cursusCol = table.AddColumn(Unit.FromCentimeter(2.4));
            //        Column descCol = table.AddColumn(Unit.FromCentimeter(3));
            //        Column cuCol = table.AddColumn(Unit.FromCentimeter(0.8));
            //        Column wvCol = table.AddColumn(Unit.FromCentimeter(1));
            //        Column toetsCodeCol = table.AddColumn(Unit.FromCentimeter(2));
            //        Column toetsStyleCol = table.AddColumn(Unit.FromCentimeter(3));
            //        Column ecCol = table.AddColumn(Unit.FromCentimeter(0.8));
            //        Column minCol = table.AddColumn(Unit.FromCentimeter(0.8));

            //        Row titleRow = table.AddRow();
            //        titleRow.Shading.Color = Colors.Red;

            //        titleRow.Cells[0].AddParagraph("Onderdeel").Format.Font.Color = Colors.White;
            //        titleRow.Cells[1].AddParagraph("Cursus").Format.Font.Color = Colors.White;
            //        titleRow.Cells[2].AddParagraph("Omschrijving").Format.Font.Color = Colors.White;
            //        titleRow.Cells[3].AddParagraph("CU").Format.Font.Color = Colors.White;
            //        titleRow.Cells[4].AddParagraph("WV").Format.Font.Color = Colors.White;
            //        titleRow.Cells[5].AddParagraph("Toetscode").Format.Font.Color = Colors.White;
            //        titleRow.Cells[6].AddParagraph("Toetsvorm").Format.Font.Color = Colors.White;
            //        titleRow.Cells[7].AddParagraph("EC").Format.Font.Color = Colors.White;
            //        titleRow.Cells[8].AddParagraph("Min").Format.Font.Color = Colors.White;

            //        int maxSchoolJaar = int.Parse(faseModules.First().FaseSchooljaar);
            //        foreach(var fm in faseModules)
            //        {
            //            if(int.Parse(fm.FaseSchooljaar) > maxSchoolJaar)
            //            {
            //                maxSchoolJaar = int.Parse(fm.FaseSchooljaar);
            //            }
            //        }

            //        string currSchoolJaar = ""+maxSchoolJaar;

            //        int totalCu = 0;
            //        decimal totalEc = 0;

            //        foreach(var fm in faseModules.Where(x => x.FaseSchooljaar.Equals(currSchoolJaar)))
            //        {
            //            Module m = fm.Module;

            //            Row startRow = table.AddRow();
            //            startRow.Format.Font.Size = 10;
            //            startRow.Cells[0].AddParagraph(m.OnderdeelCode ?? "");
            //            startRow.Cells[1].AddParagraph(m.CursusCode ?? "");
            //            startRow.Cells[2].AddParagraph(m.Naam ?? "");

            //            int cu = m.StudieBelastingen.Sum(x => x.ContactUren.Value);
            //            totalCu += cu;

            //            startRow.Cells[3].AddParagraph("" + cu);

            //            string wv = "";
            //            bool firstIteration = true;
            //            foreach (ModuleWerkvorm mwv in m.ModuleWerkvormen)
            //            {
            //                if (firstIteration) { firstIteration = false; }
            //                else { wv = wv + ", "; }
            //                wv = wv + (mwv.Werkvorm.Type ?? "NOT FOUND");
            //            }

            //            startRow.Cells[4].AddParagraph(wv);

            //            int iteration = 0;
            //            foreach(StudiePunten sp in m.StudiePunten)
            //            {
            //                if (iteration == 0)
            //                {
            //                    startRow.Cells[5].AddParagraph(sp.ToetsCode ?? "");
            //                    startRow.Cells[6].AddParagraph(sp.Toetsvorm ?? "");
            //                    startRow.Cells[7].AddParagraph(""+sp.EC);
            //                    startRow.Cells[8].AddParagraph(sp.Minimum ?? "");
            //                    firstIteration = false;
            //                }
            //                else 
            //                {
            //                    Row toetsRow = table.AddRow();
            //                    toetsRow.Format.Font.Size = 10;
            //                    toetsRow.Cells[5].AddParagraph(sp.ToetsCode ?? "");
            //                    toetsRow.Cells[6].AddParagraph(sp.Toetsvorm ?? "");
            //                    toetsRow.Cells[7].AddParagraph(""+sp.EC);
            //                    toetsRow.Cells[8].AddParagraph(sp.Minimum ?? "");
            //                }
            //                totalEc += sp.EC;

            //                iteration++;
            //            }

            //            if (iteration > 1) 
            //            {
            //                int down = iteration -1;
            //                startRow.Cells[0].MergeDown = down;
            //                startRow.Cells[1].MergeDown = down;
            //                startRow.Cells[2].MergeDown = down;
            //                startRow.Cells[3].MergeDown = down;
            //                startRow.Cells[4].MergeDown = down;
            //            }
            //        }

            //        Row lastRow = table.AddRow();
            //        lastRow.Shading.Color = Colors.LightBlue;
            //        lastRow.Cells[2].AddParagraph("Totaal").Format.Font.Bold = true;
            //        lastRow.Cells[3].AddParagraph("" + totalCu).Format.Font.Bold = true;
            //        lastRow.Cells[6].AddParagraph("Totaal").Format.Font.Bold = true;
            //        lastRow.Cells[7].AddParagraph("" + totalEc).Format.Font.Bold = true;
            //    }
            //}

            //return sect;
            throw new NotImplementedException();
        }
    }
}

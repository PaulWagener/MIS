using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Exporters;
using ModuleManager.BusinessLogic.Factories;
using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Domain;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Services
{
    public class LessenTabelExporterService : AbstractExporterService, IExporterService<FaseType>
    {

        IExporter<FaseType> lessenTabelExporterStrategy;

        public LessenTabelExporterService() 
        {
            //can't be null.
            lessenTabelExporterStrategy = new LessenTabelPassiveExporter();
        }

        /// <summary>
        /// Basic export function to generate PDF from one element
        /// </summary>
        /// <param name="toExport">element to export</param>
        /// <returns>A PDF Document</returns>
        public PdfDocument Export(FaseType toExport)
        {
            Document prePdf = new Document();

            //Document markup
            DefineStyles(prePdf);
            BuildCover(prePdf, toExport.Type);

            //Here starts the real exporting
            Section sect = prePdf.AddSection();

            LessenTabelExportArguments opt = new LessenTabelExportArguments() { ExportAll = true };

            LessenTabelExporterFactory ltef = new LessenTabelExporterFactory();
            lessenTabelExporterStrategy = ltef.GetStrategy(opt);

            try
            {
                sect = lessenTabelExporterStrategy.Export(toExport, sect);
            }
            catch (Exception e)
            {
                sect.AddParagraph("An error has occured while invoking an export-function on FaseType: " + toExport.Type + "\n" + e.Message, "error");
            }

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            rend.Document = prePdf;
            rend.RenderDocument();
            return rend.PdfDocument;
        }

        /// <summary>
        /// Basic export function to generate PDF from a list of elements
        /// </summary>
        /// <param name="pack">pack containing elements to export and arguments to specify the format</param>
        /// <returns>A PDF Document</returns>
        public PdfDocument ExportAll(IExportablePack<FaseType> pack)
        {
            Document prePdf = new Document();

            //Document markup
            DefineStyles(prePdf);
            BuildCover(prePdf, "Module-Overzicht voor Informatica");
            DefineTableOfContents(prePdf, pack.ToExport);

            //Here starts the real exporting

            LessenTabelExporterFactory ltef = new LessenTabelExporterFactory();
            lessenTabelExporterStrategy = ltef.GetStrategy(pack.Options as LessenTabelExportArguments);

            foreach (FaseType ft in pack.ToExport)
            {
                Section sect = prePdf.AddSection();
                try
                {
                    sect = lessenTabelExporterStrategy.Export(ft, sect);
                }
                catch (Exception e)
                {
                    sect.AddParagraph("An error has occured while invoking an export-function on FaseType: " + ft.Type + "\n" + e.Message, "error");
                }

                //Page numbers (only for multi-export)
                Paragraph p = new Paragraph();
                p.AddPageField();
                sect.Footers.Primary.Add(p);
                sect.Footers.EvenPage.Add(p.Clone());
            }

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            rend.Document = prePdf;
            rend.RenderDocument();
            return rend.PdfDocument;
        }

        /// <summary>
        /// Encaspulated single export as stream for downloading
        /// </summary>
        /// <param name="toExport">element to export</param>
        /// <returns>Stream to offer as download</returns>
        public BufferedStream ExportAsStream(FaseType toExport)
        {
            MemoryStream ms = new MemoryStream();
            Export(toExport).Save(ms, false);
            byte[] bytes = ms.ToArray();

            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;

            return new BufferedStream(ms);
        }

        /// <summary>
        /// Encaspulated multi export as stream for downloading
        /// </summary>
        /// <param name="pack"></param>
        /// <returns>Stream to offer as download</returns>
        public BufferedStream ExportAllAsStream(IExportablePack<FaseType> pack)
        {
            MemoryStream ms = new MemoryStream();
            ExportAll(pack).Save(ms, false);
            byte[] bytes = ms.ToArray();

            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;

            return new BufferedStream(ms);
        }

        /// <summary>
        /// Builds a table of contents, based on the content
        /// </summary>
        /// <param name="doc">The document to define for</param>
        /// <param name="contents">The content</param>
        private void DefineTableOfContents(Document doc, IEnumerable<FaseType> contents)
        {
            Section sect = doc.LastSection;

            sect.AddPageBreak();
            Paragraph p = sect.AddParagraph("Inhoudsopgave", "Heading1");
            p.Format.SpaceAfter = 24;
            p.Format.OutlineLevel = OutlineLevel.Level1;

            foreach (FaseType ft in contents)
            {
                Paragraph p2 = sect.AddParagraph();
                p2.Style = "TOC";

                if (ft.Type != null)
                {
                    Hyperlink hyperlink = p2.AddHyperlink(ft.Type);
                    hyperlink.AddText(ft.Type + "\t");
                    hyperlink.AddPageRefField(ft.Type);
                }
                else 
                {
                    p2.AddFormattedText("Critical data incomplete", "error");
                }
            }
        }
    }
}

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Exporters;
using ModuleManager.BusinessLogic.Factories;
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
    public class CompetentieExporterService : AbstractExporterService, IExporterService<Competentie>
    {
        IExporter<Competentie> competentieExporterStrategy;

        /// <summary>
        /// Constructor
        /// </summary>
        public CompetentieExporterService() 
        {
            //can't be null.
            competentieExporterStrategy = new CompetentiePassiveExporter();
        }

        /// <summary>
        /// Basic export function to generate PDF from one element
        /// </summary>
        /// <param name="toExport">element to export</param>
        /// <returns>A PDF Document</returns>
        public PdfDocument Export(Competentie toExport)
        {
            Document prePdf = new Document();

            //Document markup
            DefineStyles(prePdf);
            BuildCover(prePdf, "Competentie: " + toExport.Naam);

            //Here starts the real exporting
            Section sect = prePdf.AddSection();

            CompetentieExportArguments opt = new CompetentieExportArguments() { ExportAll = true };

            CompetentieExporterFactory cef = new CompetentieExporterFactory();
            competentieExporterStrategy = cef.GetStrategy(opt);

            try
            {
                sect = competentieExporterStrategy.Export(toExport, sect);
            }
            catch (Exception e)
            {
                sect.AddParagraph("An error has occured while invoking an export-function on Competentie: " + toExport.Naam + "\n" + e.Message, "error");
            }

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false);
            rend.Document = prePdf;
            rend.RenderDocument();
            return rend.PdfDocument;
        }

        /// <summary>
        /// Basic export function to generate PDF from a list of elements
        /// </summary>
        /// <param name="pack">pack containing elements to export and arguments to specify the format</param>
        /// <returns>A PDF Document</returns>
        public PdfDocument ExportAll(Interfaces.IExportablePack<Competentie> pack)
        {
            Document prePdf = new Document();

            //Document markup
            DefineStyles(prePdf);
            BuildCover(prePdf, "Competentie-Overzicht voor Informatica");
            DefineTableOfContents(prePdf, pack.ToExport);

            //Here starts the real exporting

            CompetentieExporterFactory cef = new CompetentieExporterFactory();
            competentieExporterStrategy = cef.GetStrategy(pack.Options as CompetentieExportArguments);

            foreach (Domain.Competentie c in pack.ToExport)
            {
                Section sect = prePdf.AddSection();
                try
                {
                    sect = competentieExporterStrategy.Export(c, sect);
                }
                catch (Exception e) 
                {
                    sect.AddParagraph("An error has occured while invoking an export-function on Competentie: " + c.Naam + "\n" + e.Message, "error");
                }
                

                //Page numbers (only for multi-export)
                Paragraph p = new Paragraph();
                p.AddPageField();
                sect.Footers.Primary.Add(p);
                sect.Footers.EvenPage.Add(p.Clone());
            }

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false);
            rend.Document = prePdf;
            rend.RenderDocument();
            return rend.PdfDocument;
        }

        /// <summary>
        /// Encaspulated single export as stream for downloading
        /// </summary>
        /// <param name="toExport">element to export</param>
        /// <returns>Stream to offer as download</returns>
        public BufferedStream ExportAsStream(Competentie toExport)
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
        /// <param name="pack">pack containing elements to export and arguments to specify the format</param>
        /// <returns>Stream to offer as download</returns>
        public BufferedStream ExportAllAsStream(Interfaces.IExportablePack<Competentie> pack)
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
        private void DefineTableOfContents(Document doc, IEnumerable<Competentie> contents)
        {
            Section sect = doc.LastSection;

            sect.AddPageBreak();
            Paragraph p = sect.AddParagraph("Inhoudsopgave", "Heading1");
            p.Format.SpaceAfter = 24;
            p.Format.OutlineLevel = OutlineLevel.Level1;

            foreach (Competentie c in contents)
            {
                Paragraph p2 = sect.AddParagraph();
                p2.Style = "TOC";
                if (c.Naam != null)
                {
                    Hyperlink hyperlink = p2.AddHyperlink(c.Naam);
                    hyperlink.AddText(c.Naam + "\t");
                    hyperlink.AddPageRefField(c.Naam);
                }
                else 
                {
                    p2.AddFormattedText("Critical data incomplete", "error");
                }
            }
        }
    }
}

using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.Domain;
using PdfSharp.Pdf;

namespace ModuleManager.BusinessLogic.Services
{
    public class DummyModuleExporterService : IExporterService<Module>
    {
        public PdfDocument Export(Module toExport)
        {
            Document pre = new Document();
            pre.AddSection();

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false);
            rend.Document = pre;
            rend.RenderDocument();
            return rend.PdfDocument;
        }

        public PdfDocument ExportAll(IExportablePack<Module> pack)
        {
            Document pre = new Document();
            pre.AddSection();

            PdfDocumentRenderer rend = new PdfDocumentRenderer(false);
            rend.Document = pre;
            rend.RenderDocument();
            return rend.PdfDocument;
        }


        public BufferedStream ExportAsStream(Module toExport)
        {
            MemoryStream ms = new MemoryStream();
            Export(toExport).Save(ms, false);
            byte[] bytes = ms.ToArray();

            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;

            return new BufferedStream(ms);
        }

        public BufferedStream ExportAllAsStream(IExportablePack<Module> pack)
        {
            MemoryStream ms = new MemoryStream();
            ExportAll(pack).Save(ms, false);
            byte[] bytes = ms.ToArray();

            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;

            return new BufferedStream(ms);
        }
    }
}

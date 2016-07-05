using ModuleManager.Domain;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Interfaces.Services
{
    /// <summary>
    /// Base class for exporting complete documents
    /// </summary>
    /// <typeparam name="T">Anything containing exportable elements</typeparam>
    public interface IExporterService<T> where T : class
    {
        /// <summary>
        /// Export a single object
        /// </summary>
        /// <param name="toExport">The object to export</param>
        /// <returns>A PDF Document containing any requested data</returns>
        PdfDocument Export(T toExport);
        /// <summary>
        /// Export multiple objects at once to the same document
        /// </summary>
        /// <param name="pack">The objects to export</param>
        /// <returns>A PDF Document containing any requested data</returns>
        PdfDocument ExportAll(IExportablePack<T> pack);

        /// <summary>
        /// Export a single object
        /// </summary>
        /// <param name="toExport">The object to export</param>
        /// <returns>A PDF Document containing any requested data</returns>
        BufferedStream ExportAsStream(T toExport);
        /// <summary>
        /// Export multiple objects at once to the same document
        /// </summary>
        /// <param name="pack">The objects to export</param>
        /// <returns>A PDF Document containing any requested data</returns>
        BufferedStream ExportAllAsStream(IExportablePack<T> pack);
    }
}

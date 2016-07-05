using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Data;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Exporters
{
    /// <summary>
    /// The base exporter, does nothing except return.
    /// </summary>
    public class ModulePassiveExporter : IExporter<Module>
    {
        /// <summary>
        /// Returns an unmodified PDF Document.
        /// </summary>
        /// <param name="toExport">The raw data from which to build the PDF</param>
        /// <param name="pdf">The document to build on</param>
        /// <returns>Unmodified input "pdf"</returns>
        public Section Export(Module toExport, Section sect)
        {
            return sect;
        }
    }
}

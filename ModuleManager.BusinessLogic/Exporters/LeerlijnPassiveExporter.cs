using MigraDoc.DocumentObjectModel;
using ModuleManager.BusinessLogic.Interfaces.Exporters;
using ModuleManager.Domain;
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
    public class LeerlijnPassiveExporter : IExporter<Leerlijn>
    {
        /// <summary>
        /// Returns an unmodified PDF Document.
        /// </summary>
        /// <param name="toExport">The raw data from which to build the PDF</param>
        /// <param name="pdf">The document to build on</param>
        /// <returns>Unmodified input "pdf"</returns>
        public Section Export(Leerlijn toExport, Section sect)
        {
            return sect;
        }
    }
}

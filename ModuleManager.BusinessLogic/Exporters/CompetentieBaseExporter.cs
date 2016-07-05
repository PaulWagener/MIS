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
    /// Base decorator pattern for exporters
    /// </summary>
    public abstract class CompetentieBaseExporter : IExporter<Competentie>
    {
        IExporter<Competentie> parent;

        /// <summary>
        /// Constructor for building the decorator pattern
        /// </summary>
        /// <param name="parent">the previous decorator pattern</param>
        public CompetentieBaseExporter(IExporter<Competentie> parent) 
        {
            this.parent = parent;
        }

        /// <summary>
        /// Abstract part of the export function, heart of the decorator pattern
        /// </summary>
        /// <param name="toExport">Data to export from</param>
        /// <param name="sect">Section to write to</param>
        /// <returns>Section with appended data</returns>
        public virtual Section Export(Competentie toExport, Section sect)
        {
            return parent.Export(toExport, sect);
        }
    }
}

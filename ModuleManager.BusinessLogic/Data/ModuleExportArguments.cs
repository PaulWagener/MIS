using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Data
{
    public class ModuleExportArguments : AbstractExportArguments
    {
        /// <summary>
        /// Export the Cursus ID
        /// </summary>
        public bool ExportCursusCode { get; set; }
        /// <summary>
        /// Export the Name
        /// </summary>
        public bool ExportNaam { get; set; }
        /// <summary>
        /// Export the Description
        /// </summary>
        public bool ExportBeschrijving { get; set; }
        /// <summary>
        /// Export any generic information
        /// </summary>
        public bool ExportAlgInfo { get; set; }
        /// <summary>
        /// Export the weight in ECs
        /// </summary>
        public bool ExportStudieBelasting { get; set; }
        /// <summary>
        /// Export the general Organisation
        /// </summary>
        public bool ExportOrganisatie { get; set; }
        /// <summary>
        /// Export the week schedule
        /// </summary>
        public bool ExportWeekplanning { get; set; }
        /// <summary>
        /// Export the reviews
        /// </summary>
        public bool ExportBeoordeling { get; set; }
        /// <summary>
        /// Export the list of educational tools
        /// </summary>
        public bool ExportLeermiddelen { get; set; }
        /// <summary>
        /// Export list of education purposes
        /// </summary>
        public bool ExportLeerdoelen { get; set; }
        /// <summary>
        /// Export the associated skill-trees
        /// </summary>
        public bool ExportLeerlijnen { get; set; }
        /// <summary>
        /// Export any associated tags
        /// </summary>
        public bool ExportTags { get; set; }
    }
}

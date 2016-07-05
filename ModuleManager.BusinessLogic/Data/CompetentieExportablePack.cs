using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Data
{
    public class CompetentieExportablePack : IExportablePack<Competentie>
    {
        CompetentieExportArguments _opt;
        IEnumerable<Competentie> _data;

        /// <summary>
        /// Constructor to make an exportable pack containing Competenties
        /// </summary>
        /// <param name="data">List of elements to export from</param>
        /// <param name="opt">What to Export</param>
        public CompetentieExportablePack(CompetentieExportArguments opt, IEnumerable<Competentie> data) 
        {
            this._data = data;
            this._opt = opt;
        }

        public AbstractExportArguments Options
        {
            get
            {
                return _opt;
            }
            set
            {
                this._opt = value as CompetentieExportArguments;
            }
        }

        public IEnumerable<Competentie> ToExport
        {
            get
            {
                return _data;
            }
            set
            {
                this._data = value;
            }
        }
    }
}

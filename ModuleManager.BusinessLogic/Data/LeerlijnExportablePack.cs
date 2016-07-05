using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Data
{
    public class LeerlijnExportablePack : IExportablePack<Leerlijn>
    {
        LeerlijnExportArguments _args;
        IEnumerable<Leerlijn> _data;

        public LeerlijnExportablePack(LeerlijnExportArguments opt, IEnumerable<Leerlijn> data) 
        {
            this._args = opt;
            this._data = data;
        }

        public AbstractExportArguments Options
        {
            get
            {
                return _args;
            }
            set
            {
                _args = value as LeerlijnExportArguments;
            }
        }

        public IEnumerable<Leerlijn> ToExport
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}

using ModuleManager.BusinessLogic.Interfaces;
using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Data
{
    /// <summary>
    /// Implementation of IQueryablePack that allows the filtering, sorting and exporting of Modules.
    /// </summary>
    /// <remarks>
    /// This implementation offers a collection of Data together with Arguments. The arguments are used to manipulate the data into a manageable chunk of data.
    /// </remarks>
    public class ModuleQueryablePack : IQueryablePack<Module>
    {
        private readonly IQueryable<Module> _data;
        private readonly ModuleFilterSorterArguments _args;

        /// <summary>
        /// The arguments to go with the Data
        /// </summary>
        public ModuleFilterSorterArguments Args
        {
            get { return _args; }
        }

        /// <summary>
        /// The Data to query
        /// </summary>
        public IQueryable<Module> Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Constructor for the Pack
        /// </summary>
        /// <param name="args">Arguments apliccable to the Data</param>
        /// <param name="data">The data to manipulate</param>
        public ModuleQueryablePack(ModuleFilterSorterArguments args, IQueryable<Module> data) 
        {
            this._data = data;
            this._args = args;
        }
    }
}

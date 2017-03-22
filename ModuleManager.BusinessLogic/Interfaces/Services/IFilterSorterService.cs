using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Interfaces.Services
{
    /// <summary>
    /// Basic Data manipulation class
    /// </summary>
    /// <typeparam name="T">Data to Manipulate</typeparam>
    public interface IFilterSorterService<T> where T:class
    {
        /// <summary>
        /// basic data manipulation function
        /// </summary>
        /// <param name="inputData">Complete pack with Data and Arguments</param>
        /// <param name="totalRecordCount">The recordcount before paging.</param>
        /// <returns>The resulting Data List</returns>
        IQueryable<T> ProcessData(IQueryablePack<T> inputData, out int totalRecordCount);
    }
}

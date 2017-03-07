using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Interfaces.Services
{
    /// <summary>
    /// Base sorter service for EVERYTHING
    /// </summary>
    /// <typeparam name="T">EVERYTHING</typeparam>
    public interface ISorterService<T> where T : class
    {
        /// <summary>
        /// Basic Sorter function template
        /// </summary>
        /// <param name="toSort">Complete pack with Data and Arguments</param>
        /// <returns>Sorted Data</returns>
        IQueryable<T> Sort(IQueryablePack<T> toSort);
    }
}

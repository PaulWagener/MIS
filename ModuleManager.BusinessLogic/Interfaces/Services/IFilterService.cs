using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Interfaces.Services
{
    /// <summary>
    /// Base Filter Service for EVERYTHING
    /// </summary>
    /// <typeparam name="T">EVERYTHING</typeparam>
    public interface IFilterService<T> where T : class
    {
        /// <summary>
        /// Basic Filter function template
        /// </summary>
        /// <param name="toQuery">Complete pack with Data and Arguments</param>
        /// <returns>Filtered Data</returns>
        IQueryable<T> Filter(IQueryablePack<T> toQuery);
    }
}

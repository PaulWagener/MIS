using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ModuleManager.Web.Helpers
{
    public static class LinqHelper
    {
        public static bool ContainsDoubles<T, TKey>(this IEnumerable<T> collection, Expression<Func<T, TKey>> groupBy)
        {
            return collection.AsQueryable()
                             .GroupBy(groupBy)
                             .Where(x => x.Skip(1).Any())
                             .Any();
        }
    }
}
using ModuleManager.Web.ViewModels;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using ModuleManager.Domain;
using AutoMapper;

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
        
        public static void ClearAndFill<TEntity>(this ICollection<TEntity> dbSet, IEnumerable<TEntity> entities) where TEntity : class
        {
            dbSet.Clear();
            foreach (TEntity e in entities)
            {
                dbSet.Add(e);
            }
        }

        public static void ClearAndFill<TEntity, TViewModel>(this ICollection<TEntity> dbSet, IEnumerable<TViewModel> viewModels)
        {
            dbSet.Clear();
            foreach (TViewModel vm in viewModels)
            {
                dbSet.Add(Mapper.Map<TViewModel, TEntity>(vm));
            }
        }

        public static void AttachAllUnchanged<TEntity>(this DbSet<TEntity> dbSet, DomainEntities context, IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                context.Entry(entity).State = EntityState.Unchanged;
                dbSet.Attach(entity);
            }
        }
    }
}
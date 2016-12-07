using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ModuleManager.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(Expression<Func<T, bool>> predicate);
        string Create(T entity);
        string Delete(T entity);
        string Edit(T entity);
    }
}
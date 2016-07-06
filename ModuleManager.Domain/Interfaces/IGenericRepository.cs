using System.Collections.Generic;

namespace ModuleManager.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(object[] keys);
        string Create(T entity);
        string Delete(T entity);
        string Edit(T entity);
    }
}
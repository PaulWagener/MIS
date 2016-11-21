using System.Collections.Generic;

namespace ModuleManager.Web.Controllers.Api.Interfaces
{
    public interface IGenericApiController<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(int schooljaar, string key);
        void Delete(T entity);
        void Edit(T entity);
        void Create(T entity); 
    }
}
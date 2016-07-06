using System;

namespace ModuleManager.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DomainEntities Context { get; }

        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
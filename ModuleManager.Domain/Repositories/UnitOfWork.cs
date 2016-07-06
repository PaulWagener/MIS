using ModuleManager.Domain.Interfaces;

namespace ModuleManager.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DomainEntities _context;

        public DomainEntities Context
        {
            get { return _context ?? (_context = new DomainEntities()); }
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using ModuleManager.DomainDAL.Interfaces;
using ModuleManager.DomainDAL.Utility;

namespace ModuleManager.DomainDAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DomainContext _context;

        public GenericRepository(DomainContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetOne(object[] keys)
        {
            return _context.Set<T>().Find(keys);
        }

        public string Create(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                    return DomainConstants.DbErrorPkDuplicate;
                if (ex is DbEntityValidationException)
                    return DomainConstants.DbErrorNoSelection;

                return DomainConstants.DbErrorStandard;
            }
            return null;
        }

        public string Delete(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                    return DomainConstants.DbErrorFkConstraint;
                if (ex is ArgumentOutOfRangeException)
                    return DomainConstants.DbErrorPkInvalid;

                return DomainConstants.DbErrorStandard;
            }
            return null;
        }

        public string Edit(T entity)
        {
            try
            {
                //_context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                    return DomainConstants.DbErrorFkConstraint;
                if (ex is InvalidOperationException)
                    return DomainConstants.DbErrorPkChanged;
                if (ex is ArgumentOutOfRangeException)
                    return DomainConstants.DbErrorPkInvalid;

                return DomainConstants.DbErrorStandard;
            }
            return null;
        }
    }
}

using ModuleManager.Domain;
using ModuleManager.UserDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.UserDAL.Repositories
{
    public class SysteemRolRepository : ISysteemRolRepository
    {
        public IEnumerable<SysteemRol> GetAll()
        {
            using (var context = new DomainEntities())
            {
                return (from s in context.SysteemRol select s).ToList();
            }

        }

    }
}

using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.UserDAL.Interfaces
{
    public interface ISysteemRolRepository
    {

        IEnumerable<SysteemRol> GetAll();
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.Domain.Utility
{
    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<DomainEntities>
    {
        protected override void Seed(DomainEntities context)
        {
            base.Seed(context);

           
        }
    }
}

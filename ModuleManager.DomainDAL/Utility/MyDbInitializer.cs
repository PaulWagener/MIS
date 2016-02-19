using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.DomainDAL.Utility
{
    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<DomainContext>
    {
        protected override void Seed(DomainContext context)
        {
            base.Seed(context);

           
        }
    }
}

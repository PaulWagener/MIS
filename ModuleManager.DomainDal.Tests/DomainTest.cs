using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ModuleManager.Domain.Tests
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void FK_Module_Werkvorm()
        {
            using(var context = new Domain.DomainEntities() )
            {
                //Act
                var modules = context.Leerlijnen.ToList();

                //Assert
            }
        }
    }
}

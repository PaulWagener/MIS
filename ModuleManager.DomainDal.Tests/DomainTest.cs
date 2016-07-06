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
                var a = context.Set<Werkvorm>().ToList();
                var b = context.Set<ModuleWerkvorm>().ToList();
                var c = context.Set<Icon>().ToList();
                var d = context.Set<StudiePunt>().ToList();
                var e = context.Set<StudieBelasting>().ToList();



                //Assert
            }
        }
    }
}

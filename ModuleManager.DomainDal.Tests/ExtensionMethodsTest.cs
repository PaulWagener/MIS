using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using ModuleManager.DomainDAL;
using ModuleManager.DomainDAL.ExtensionMethods;
using System.Collections.Generic;

namespace ModuleManager.DomainDal.Tests
{
    [TestClass]
    [Ignore]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void ExtensionMethods_UpdateWith_Success()
        {
            ////1. arrange
            //ICollection<Leerlijn> oldSet = new List<Leerlijn>();
            //oldSet.Add(new Leerlijn { Naam = "a", Schooljaar = "b" });
            //oldSet.Add(new Leerlijn { Naam = "c", Schooljaar = "d" });
   

            //ICollection<Leerlijn> newSet = new List<Leerlijn>();
            //newSet.Add(new Leerlijn { Naam = "c", Schooljaar = "d" });
            //newSet.Add(new Leerlijn { Naam = "e", Schooljaar = "f" });

            ////2. Act
            //oldSet.UpdateWith<Leerlijn>(newSet);

            ////3. Assert
            //Assert.AreEqual(3, oldSet.Count);
            //Assert.IsTrue(oldSet.Contains(new Leerlijn { Naam = "c", Schooljaar = "d" }));
            //Assert.IsTrue(oldSet.Contains(new Leerlijn { Naam = "e", Schooljaar = "f" }));
        }
    }
}

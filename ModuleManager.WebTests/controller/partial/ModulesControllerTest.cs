﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Domain.Repositories;
using ModuleManager.Web.Controllers.PartialViewControllers;
using ModuleManager.Web.ViewModels.PartialViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModuleManager.WebTests.controller.partial
{
    [TestClass]
    public class ModulesControllerTest
    {
        private ModulesController controller;
        Mock<IUnitOfWork> unitMock;
        Module testModule;
        Module resultModule;
        ModuleCrudViewModel testVM; 

        [TestInitialize]
        public void Init()
        {
            testModule = new Module()
            {
                CursusCode = "a",
                Schooljaar = 1516,
                Verantwoordelijke = new Docent() { Naam = "Stijn " },
                Status = "d",
                Icon = "e",
                Onderdeel = new Onderdeel() {  Code = "h" },
                StudiePunten = new List<StudiePunt>()
                {
                    new StudiePunt() { ToetsCode = "i", EC = 1},
                    new StudiePunt() { ToetsCode = "j", EC = 2}
                },
                Naam = "k",

            };

            testVM = new ModuleCrudViewModel()
            {
                Naam = "l",
                Toetscode1 = "i",
                Toetscode2 = "n",
                Ec1 = 1,
                Ec2 = 3,
                //Verantwoordelijke = "o",
                Onderdeel = "q",
                Icon = "r"
            };

            unitMock = new Mock<IUnitOfWork>();
            controller = new ModulesController(unitMock.Object);

            unitMock.Setup(u => u.GetRepository<Module>().GetOne(It.IsAny<Expression<Func<Module, bool>>>())).Returns(testModule);
            unitMock.Setup(u => u.GetRepository<Module>().Edit(It.IsAny<Module>()))
                .Callback<Module>((module) => resultModule = module);

        }


        
        [TestMethod]
        [TestCategory("Edit")]
        public void ModulesController_Edit_Success()
        {
            //1. arrange
            //using default from init

            //2. act
            var result = controller.Edit(testVM);

            //3. assert
            unitMock.Verify(u => u.GetRepository<Module>().Edit(It.IsAny<Module>()), Times.Exactly(2));
            Assert.AreEqual("l", resultModule.Naam);
            Assert.AreEqual(2, resultModule.StudiePunten.Count);
            Assert.IsTrue(resultModule.StudiePunten.Any(s => s.ToetsCode == "i"));
            Assert.IsTrue(resultModule.StudiePunten.Any(s => s.EC == 1));
            Assert.IsTrue(resultModule.StudiePunten.Any(s => s.ToetsCode == "n"));
            Assert.IsTrue(resultModule.StudiePunten.Any(s => s.EC == 3));
            Assert.AreEqual("o", resultModule.Verantwoordelijke.Naam);
            Assert.AreEqual("q", resultModule.OnderdeelCode);
            Assert.AreEqual("r", resultModule.Icon);
        }
    }
}

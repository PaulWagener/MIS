using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleManager.Web.Controllers;
using ModuleManager.DomainDAL.Repositories;
using ModuleManager.DomainDAL.Interfaces;
using ModuleManager.DomainDAL;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ModuleManager.WebTests
{
    [TestClass]
    public class ModuleControllerTest
    {
        private ModuleController controller;
        IUnitOfWork unit;

        [TestInitialize]
        public void Init()
        {
            unit = new UnitOfWork();

     
            //arrange test module
            TestData.TestData.DefaultTestData(unit.Context);

            unit.Context.Dispose();
            unit = new UnitOfWork();

            //Create controller
            controller = new ModuleController(null, null, unit);

        }

        /**
         * Ontbrekend in deze test methode
         * FaseModule -> Geen UI scherm voor edit
         * Competentie -> UI leverd niet de goeie data
         **/
        [TestMethod]
        public void ModuleController_Edit_Success()
        {
            //1. Arrange
            var vm = new ModuleViewModel()
            {
             
                CursusCode = "Test1", //key to find test data
                Schooljaar = "1516",  //key to find test data
                Beschrijving = "A",
                Icon = "Database",
                Beoordelingen = new List<BeoordelingenViewModel>()
                {
                    new BeoordelingenViewModel()
                    {
                        Beschrijving = "Theorie Toets",
                    }
                },
                Leerlijn = new List<LeerlijnViewModel>()
                {
                    new LeerlijnViewModel(){ Naam = "Programmeren", Schooljaar = "1516" },
                    new LeerlijnViewModel(){ Naam = "Modeleren", Schooljaar = "1516" }
                },
                Leerdoelen = new List<LeerdoelenViewModel>()
                {
                    new LeerdoelenViewModel(){ Beschrijving = "Werken met frameworks" }
                    
                },
                Leermiddelen = new List<LeermiddelenViewModel>()
                {
                    new LeermiddelenViewModel(){ Beschrijving = "computer" }
                },
                Docent = new List<DocentViewModel>()
                {
                    new DocentViewModel(){ Name = "Stijn Smulders"}
                },
                ModuleWerkvorm = new List<ModuleWerkvormViewModel>()
                {
                    new ModuleWerkvormViewModel(){ WerkvormType = "WS", Organisatie = "6 workshops"}
                },
                Status = "Incompleet",
                StudieBelasting = new List<StudieBelastingViewModel>()
                {
                    new StudieBelastingViewModel() 
                    { Activiteit = "Huiswerk", ContactUren = 22, Duur = "6 weken", Frequentie = "1x per week"}
                },
                //StudiePunten = new List<StudiePuntenViewModel>()
                //{
                //    new StudiePuntenViewModel() { Toetsvorm = "Assessment", EC = 2, Minimum = "5.5", ToetsCode = "PROG5PR" }

                //},
                Tag = new List<TagViewModel>()
                {
                    new TagViewModel(){ Naam = "MVC", Schooljaar = "1516"  }
                },
                Verantwoordelijke = "E. Test",
                Weekplanning = new List<WeekplanningViewModel>()
                {
                    new WeekplanningViewModel() { Onderwerp = "C#", Week = "1" }
                }


                
            };

            //2. Act
            controller.Edit(new ModuleEditViewModel(vm));
    

            //3. Assert
            Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
            
            Assert.AreEqual("A", module.Beschrijving);
           // Assert.AreEqual("2", module.OnderdeelCode);
            Assert.AreEqual("Nieuw", module.Status);

            Assert.AreEqual(2, module.Leerlijn.Count);
            Assert.AreEqual(1, module.Beoordelingen.Count);
            Assert.AreEqual("Theorie Toets", module.Beoordelingen.First().Beschrijving);
            Assert.AreEqual(1, module.Leerdoelen.Count);
            Assert.AreEqual("Werken met frameworks", module.Leerdoelen.First().Beschrijving);
            Assert.AreEqual(1, module.Leermiddelen.Count);
            Assert.AreEqual("computer", module.Leermiddelen.First().Beschrijving);
            Assert.AreEqual(1, module.Docent.Count);
            Assert.AreEqual("Stijn Smulders", module.Docent.First().Name);
            Assert.AreEqual(1, module.ModuleWerkvorm.Count);
            Assert.AreEqual("WS", module.ModuleWerkvorm.First().WerkvormType);
            Assert.AreEqual(1, module.StudieBelasting.Count);
            Assert.AreEqual("Huiswerk", module.StudieBelasting.First().Activiteit);
            Assert.AreEqual("1516", module.StudieBelasting.First().Schooljaar);

            //Assert.AreEqual(1, module.StudiePunten.Count);
            //Assert.AreEqual("Assessment", module.StudiePunten.First().Toetsvorm);
            //Assert.AreEqual("2", module.StudiePunten.First().EC);
            //Assert.AreEqual("PROG5PR", module.StudiePunten.First().ToetsCode);
            //Assert.AreEqual("1516", module.StudiePunten.First().Schooljaar);
            
            Assert.AreEqual(1, module.Tag.Count);
            Assert.AreEqual("MVC", module.Tag.First().Naam);

            Assert.AreEqual(1, module.Weekplanning.Count);
            Assert.AreEqual("C#", module.Weekplanning.First().Onderwerp);
            Assert.AreEqual("1", module.Weekplanning.First().Week);
        }
    }
}

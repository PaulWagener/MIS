using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleManager.Web.Controllers;
using ModuleManager.Domain.Repositories;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using AutoMapper;
using ModuleManager.Web.ViewModels.PartialViewModel;


namespace ModuleManager.WebTests
{
    [TestClass]
    public class ModuleControllerTest
    {
        private ModuleController controller;
        IUnitOfWork unit;
        TestData testData;

        [TestInitialize]
        public void Init()
        {
            unit = new UnitOfWork();
            controller = new ModuleController(null, null, unit);
            testData = new TestData();
            testData.DeleteTestData(); //first we empty the DB
            testData.CreateTestData(); //now we can start creating new data
        }


        public ModuleEditViewModel GetModuleEditViewModel()
        {
            var vm = new ModuleEditViewModel();
            vm.Module = new ModuleViewModel();
            vm.Module.CursusCode = testData.Module.CursusCode; //copy key
            vm.Module.Schooljaar = testData.Module.Schooljaar; //copy key
            return vm;
        }

        /**
         * Ontbrekend in deze test methode
         * FaseModule -> Geen UI scherm voor edit
         * Competentie -> UI leverd niet de goeie data
         **/
        [TestMethod]
        public void ModuleController_Edit_Beschrijving_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Beschrijving = "a";
              
            //2. Act
            controller.Edit(vm);
           
            using (var context = new DomainDalEntities())
            {
               //3. Assert (alwasy in new context
               Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
               Assert.AreEqual("a", module.Beschrijving);
            }
        }

        /**
       * Ontbrekend in deze test methode
       * FaseModule -> Geen UI scherm voor edit
       * Competentie -> UI leverd niet de goeie data
       **/
        [TestMethod]
        public void ModuleController_Edit_Leerlijnen_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();

            vm.Module.Leerlijn.Add( new LeerlijnViewModel(){ Naam = "Programmeren", Schooljaar = "1516" } );
            vm.Module.Leerlijn.Add(new LeerlijnViewModel() { Naam = "Architectuur", Schooljaar = "1516" });

            //2. Act
            controller.Edit(vm);


            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Leerlijn.Count);
                Assert.IsTrue(module.Leerlijn.Any(l => l.Naam == "Programmeren"));
                Assert.IsTrue(module.Leerlijn.Any(l => l.Naam == "Architectuur"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_Tags_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Tag.Add(new TagViewModel() { Naam = "WCF",  });
            vm.Module.Tag.Add(new TagViewModel() { Naam = "MVVM", });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Tag.Count);
                Assert.IsTrue(module.Tag.Any(l => l.Naam == "MVVM"));
                Assert.IsTrue(module.Tag.Any(l => l.Naam == "WCF"));
            }
        }

        
        [TestMethod]
        public void ModuleController_Edit_Leerdoel_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Leerdoelen.Add(new LeerdoelenViewModel() { Beschrijving = "Leerdoel 2", CursusCode = "Test1", Schooljaar = "1516", Id = 2 });
            vm.Module.Leerdoelen.Add( new LeerdoelenViewModel(){ Beschrijving = "Leerdoel 3", CursusCode = "Test1", Schooljaar = "1516"});

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Leerdoelen.Count);
                Assert.IsTrue(module.Leerdoelen.Any(l => l.Id == 2));
                Assert.IsTrue(module.Leerdoelen.Any(l => l.Beschrijving == "Leerdoel 2"));
                Assert.IsTrue(module.Leerdoelen.Any(l => l.Beschrijving == "Leerdoel 3"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_Leermiddel_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Leermiddelen.Add( new LeermiddelenViewModel(){ Beschrijving = "Leermiddel 2", CursusCode = "Test1", Schooljaar = "1516",  Id = 2 });
            vm.Module.Leermiddelen.Add( new LeermiddelenViewModel(){ Beschrijving = "Leermiddel 3", CursusCode = "Test1", Schooljaar = "1516" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Leermiddelen.Count);
                Assert.IsTrue(module.Leermiddelen.Any(l => l.Id == 2));
                Assert.IsTrue(module.Leermiddelen.Any(l => l.Beschrijving == "Leermiddel 2"));
                Assert.IsTrue(module.Leermiddelen.Any(l => l.Beschrijving == "Leermiddel 3"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_StudieBelasting_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.StudieBelasting.Add( new StudieBelastingViewModel()
                   { Activiteit = "A2", ContactUren = 22, Duur = "2 weken", Frequentie = "2x per week", CursusCode = "Test1", Schooljaar = "1516"});
            vm.Module.StudieBelasting.Add( new StudieBelastingViewModel()
                   { Activiteit = "A3", ContactUren = 22, Duur = "3 weken", Frequentie = "3x per week", CursusCode = "Test1", Schooljaar = "1516"});

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.StudieBelasting.Count);
                Assert.IsTrue(module.StudieBelasting.Any(s => s.Activiteit == "A2"));
                Assert.IsTrue(module.StudieBelasting.Any(l => l.Activiteit == "A3"));
            }
        }


        [TestMethod]
        public void ModuleController_Edit_Weekplanning_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Weekplanning.Add(new WeekplanningViewModel() { Id  = 2, Onderwerp = "twee", Week = "2", CursusCode = "Test1", Schooljaar = "1516" });
            vm.Module.Weekplanning.Add(new WeekplanningViewModel() { Onderwerp = "drie", Week = "3", CursusCode = "Test1", Schooljaar = "1516" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Weekplanning.Count);
                Assert.IsTrue(module.Weekplanning.Any(l => l.Id == 2));
                Assert.IsTrue(module.Weekplanning.Any(l => l.Onderwerp == "twee"));
                Assert.IsTrue(module.Weekplanning.Any(l => l.Onderwerp == "drie"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_Weekplanning_Fail() {
          //1. Arrange
          var vm = GetModuleEditViewModel();
          vm.Module.Weekplanning.Add(new WeekplanningViewModel() {
            //geen weeknummer
            Id = 2,
            Onderwerp = "twee",
            CursusCode = "DB1",
            Schooljaar = "1516"
          });
          vm.Module.Weekplanning.Add(new WeekplanningViewModel() {
            // geen onderwerp
            Week = "3",
            CursusCode = "DB1",
            Schooljaar = "1516"
          });

          //2. Act
          controller.Edit(vm);

          using (var context = new DomainDalEntities()) {
            //3. Assert (always in new context
            Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "DB1");
            Assert.AreEqual(0, module.Weekplanning.Count);
          }
        }

        [TestMethod]
        public void ModuleController_Edit_Docenten_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Docenten.Add(new DocentViewModel() { Id = 2 });
            vm.Module.Docenten.Add(new DocentViewModel() { Id = 3 });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Docenten.Count);
                Assert.IsTrue(module.Docenten.Any(l => l.Id == 2));
                Assert.IsTrue(module.Docenten.Any(l => l.Id == 3));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_Competenties_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.ModuleCompetentie.Add(new ModuleCompetentieViewModel() { CompetentieCode = "BC2", Niveau = "Beginner" });
            vm.Module.ModuleCompetentie.Add(new ModuleCompetentieViewModel() { CompetentieCode = "BC3", Niveau = "Expert" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.ModuleCompetentie.Count);
                Assert.IsTrue(module.ModuleCompetentie.Any(l => l.CompetentieCode == "BC2"));
                Assert.IsTrue(module.ModuleCompetentie.Any(l => l.CompetentieCode == "BC3"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_IncompleteCompetentie_Ignore()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.ModuleCompetentie.Add(new ModuleCompetentieViewModel() { CompetentieCode = null, Niveau = "Beginner" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(0, module.ModuleCompetentie.Count);
            }
        }


        [TestMethod]
        public void ModuleController_Edit_ModuleWerkvorm_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.ModuleWerkvorm.Add(new ModuleWerkvormViewModel() { WerkvormType = "WS", Organisatie = "2 workshops", CursusCode = "Test1", Schooljaar = "1516" });
            vm.Module.ModuleWerkvorm.Add(new ModuleWerkvormViewModel() { WerkvormType = "PR", Organisatie = "3 Practicums", CursusCode = "Test1", Schooljaar = "1516" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.ModuleWerkvorm.Count);
                Assert.IsTrue(module.ModuleWerkvorm.Any(l => l.WerkvormType == "WS"));
                Assert.IsTrue(module.ModuleWerkvorm.Any(l => l.WerkvormType == "PR"));
            }
        }

        [TestMethod]
        public void ModuleController_Edit_Beoordelingen_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.Beoordelingen.Add(new BeoordelingenViewModel() { Id = 2, Beschrijving = "b2", CursusCode = "Test1", Schooljaar = "1516" });
            vm.Module.Beoordelingen.Add(new BeoordelingenViewModel() { Beschrijving = "b3", CursusCode = "Test1", Schooljaar = "1516" });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(2, module.Beoordelingen.Count);
                Assert.IsTrue(module.Beoordelingen.Any(l => l.Id == 2));
                Assert.IsTrue(module.Beoordelingen.Any(l => l.Beschrijving == "b2"));
                Assert.IsTrue(module.Beoordelingen.Any(l => l.Beschrijving == "b3"));
            }
        }

               [TestMethod]
        public void ModuleController_Edit_Voorkennis_Success()
        {
            //1. Arrange
            var vm = GetModuleEditViewModel();
            vm.Module.VoorkennisModules.Add(new ModuleVoorkennisViewModel() { Schooljaar = "1516", CursusCode = "PROG1", });

            //2. Act
            controller.Edit(vm);

            using (var context = new DomainDalEntities())
            {
                //3. Assert (alwasy in new context
                Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
                Assert.AreEqual(1, module.Voorkennis.Count);
                Assert.IsTrue(module.Voorkennis.Any(l => l.CursusCode == "PROG1"));
            }
        }
         

        [TestMethod]
        [Ignore]
        public void Legacy()
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
                    new LeerlijnViewModel(){ Naam = "Testen", Schooljaar = "1516" }
                },
                Leerdoelen = new List<LeerdoelenViewModel>()
                {
                    new LeerdoelenViewModel(){ Beschrijving = "Werken met frameworks" }
                    
                },
                Leermiddelen = new List<LeermiddelenViewModel>()
                {
                    new LeermiddelenViewModel(){ Beschrijving = "computer" }
                },
                //Docent = new List<DocentViewModel>()
                //{
                //    new DocentViewModel(){ Name = "Stijn Smulders"}
                //},
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
                    new TagViewModel(){ Naam = "MVC" },
                    new TagViewModel(){ Naam = "Architectuur" }
                    
                },
                Verantwoordelijke = "E. Test",
                Weekplanning = new List<WeekplanningViewModel>()
                {
                    new WeekplanningViewModel() { Onderwerp = "C#", Week = "1" }
                }
            };

            //2. Act
            controller.Edit(new ModuleEditViewModel(vm));

            unit.Context.Dispose();
            unit = new UnitOfWork();
    

            //3. Assert
            Module module = unit.Context.Module.First(m => m.Schooljaar == "1516" && m.CursusCode == "Test1");
            
            Assert.AreEqual("A", module.Beschrijving);
            Assert.AreEqual(2, module.Leerlijn.Count);

            //Assert.AreEqual(1, module.Beoordelingen.Count);
            //Assert.AreEqual("Theorie Toets", module.Beoordelingen.First().Beschrijving);
            //Assert.AreEqual(1, module.Leerdoelen.Count);
            //Assert.AreEqual("Werken met frameworks", module.Leerdoelen.First().Beschrijving);
            //Assert.AreEqual(1, module.Leermiddelen.Count);
            //Assert.AreEqual("computer", module.Leermiddelen.First().Beschrijving);
            ////Assert.AreEqual(1, module.Docent.Count);
            ////Assert.AreEqual("Stijn Smulders", module.Docent.First().Name);
            //Assert.AreEqual(1, module.ModuleWerkvorm.Count);
            //Assert.AreEqual("WS", module.ModuleWerkvorm.First().WerkvormType);
            //Assert.AreEqual(1, module.StudieBelasting.Count);
            //Assert.AreEqual("Huiswerk", module.StudieBelasting.First().Activiteit);
            //Assert.AreEqual("1516", module.StudieBelasting.First().Schooljaar);

            //Assert.AreEqual(1, module.StudiePunten.Count);
            //Assert.AreEqual("Assessment", module.StudiePunten.First().Toetsvorm);
            //Assert.AreEqual("2", module.StudiePunten.First().EC);
            //Assert.AreEqual("PROG5PR", module.StudiePunten.First().ToetsCode);
            //Assert.AreEqual("1516", module.StudiePunten.First().Schooljaar);
            
            //Assert.AreEqual(2, module.Tag.Count);
            //Assert.IsTrue(module.Tag.Any(t => t.Naam == "Architectuur"));
            //Assert.IsTrue(module.Tag.Any(t => t.Naam == "Architectuur"));

            //Assert.AreEqual(1, module.Weekplanning.Count);
            //Assert.AreEqual("C#", module.Weekplanning.First().Onderwerp);
            //Assert.AreEqual("1", module.Weekplanning.First().Week);
        }
    }
}

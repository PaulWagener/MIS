using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModuleManager.WebTests
{
    public class TestData
    {
        public Module Module { get; set; }

        public void CreateTestData()
        {
            using(var context = new DomainEntities())
            {
                var leerlijn_prog = new Leerlijn() { Naam = "Programmeren", Schooljaar = "1516" };
                var leerlijn_mod = new Leerlijn() { Naam = "Modelleren", Schooljaar = "1516" };
                var leerlijn_arch = new Leerlijn() { Naam = "Architectuur", Schooljaar = "1516" };

                var tag_mvc = new Tag() { Naam = "MVC"};
                var tag_mvvm = new Tag() { Naam = "MVVM"};
                var tag_wcf = new Tag() { Naam = "WCF" };

                var docent_stijn = new Docent() { Id = 1, Naam = "Stijn Smulders" };
                var docent_bart = new Docent() { Id = 2, Naam = "Bart Mutsaers" };
                var docent_ger = new Docent() { Id = 3, Naam = "Ger Saris" };

                var competentie_bc1 = new Competentie() { Schooljaar = "1516", Code = "BC1", Naam = "Procesanalyse uitvoeren", Beschrijving = "Stelt de student in staat.. " };
                var competentie_bc2 = new Competentie() { Schooljaar = "1516", Code = "BC2", Naam = "Testplan opzetten", Beschrijving = "Stelt de student in staat.. " };
                var competentie_bc3 = new Competentie() { Schooljaar = "1516", Code = "BC3", Naam = "Technisch ontwerp", Beschrijving = "Stelt de student in staat.. " };

                var prog1 = new Domain.Module()
                 {
                     CursusCode = "PROG1", //key
                     Schooljaar = "1516", //key
                     Verantwoordelijke = "P. Rog",
                     Naam = "Programmeren 1",
                     Beschrijving = "building software",
                     Icon = "database",
                     OnderdeelCode = "ABV1",
                     Status = "Nieuw",
                     
                 };

                var db1 = new Domain.Module()
                {
                    CursusCode = "DB1", //key
                    Schooljaar = "1516", //key
                    Verantwoordelijke = "D. Atab",
                    Naam = "Databases 1",
                    Beschrijving = "building databases",
                    Icon = "code",
                    OnderdeelCode = "ABV1",
                    Status = "Nieuw",
                };

                Module = new Domain.Module()
                {
                    CursusCode = "Test1", //key
                    Schooljaar = "1516", //key
                    Naam = "Testing 101",
                    Verantwoordelijke = "T. Est",
                    Beschrijving = "Testing if software works",
                    Icon = "code",
                    OnderdeelCode = "ABV1",
                    Status = "Nieuw",
                    /** Many to Many **/
                    Leerlijnen = new List<Leerlijn>() { leerlijn_prog, leerlijn_mod },
                    Tags = new List<Tag>() { tag_mvc, tag_mvvm },
                    Voorkennis = new List<Module>{ db1 },
                    Docenten = new List<Docent>() {  docent_stijn, docent_bart },
                    /** One to Many **/
                    Leerdoelen = new List<Leerdoel>(){ 
                        new Leerdoel(){ Beschrijving = "Leerdoel 1", Schooljaar = "1516"},
                        new Leerdoel(){ Beschrijving = "Leerdoel 2", Schooljaar = "1516"}
                    },
                    Leermiddelen = new List<Leermiddel>()
                    {
                        new Leermiddel(){ Beschrijving = "Leermiddel 1", Schooljaar = "1516"},
                        new Leermiddel(){ Beschrijving = "Leermiddel 2", Schooljaar = "1516"}
                    },
                    Weekplanningen = new List<Weekplanning>()
                    {
                        new Weekplanning() { Schooljaar = "1516", Week = "1", Onderwerp = "een" },
                        new Weekplanning() { Schooljaar = "1516", Week = "2", Onderwerp = "twee" },
                    },
                    Beoordelingen = new List<Beoordeling>()
                    {
                        new Beoordeling() {Schooljaar = "1516", Beschrijving = "b1" },
                        new Beoordeling() {Schooljaar = "1516", Beschrijving = "b2" },
                    },
                    ModuleWerkvormen = new List<ModuleWerkvorm>()
                    {
                       new ModuleWerkvorm() { WerkvormType = "WS", Organisatie = "2 workshops", CursusCode = "Test1", Schooljaar = "1516" },
                    },
                    StudieBelastingen = new List<StudieBelasting>()
                    {
                        new StudieBelasting() { Activiteit = "A1", ContactUren = 11, Duur = "1 weken", Frequentie = "1x per week",  Schooljaar = "1516" },
                        new StudieBelasting() { Activiteit = "A2", ContactUren = 22, Duur = "2 weken", Frequentie = "2x per week",  Schooljaar = "1516" },
                    },
                    ModuleCompetenties = new List<ModuleCompetentie>()
                    {
                        new ModuleCompetentie() { CompetentieCode = "BC1", Niveau = "Beginner", Schooljaar = "1516", CompetentieSchooljaar = "1516" },
                        new ModuleCompetentie() { CompetentieCode = "BC2", Niveau = "Expert", Schooljaar = "1516" ,  CompetentieSchooljaar = "1516"}
                    }

                };

                context.Competenties.Add(competentie_bc1);
                context.Competenties.Add(competentie_bc2);
                context.Competenties.Add(competentie_bc3);
                context.Leerlijnen.Add(leerlijn_prog);
                context.Leerlijnen.Add(leerlijn_mod);
                context.Leerlijnen.Add(leerlijn_arch);
                context.Tags.Add(tag_mvc);
                context.Tags.Add(tag_mvvm);
                context.Tags.Add(tag_wcf);
                context.Modules.Add(prog1);
                context.Modules.Add(db1);
                context.Modules.Add(Module);
                context.Docenten.Add(docent_bart);
                context.Docenten.Add(docent_ger);
                context.Docenten.Add(docent_stijn);

                try
                {
                    context.SaveChanges();
                }
                catch 
                {
                    //Krijg je Entity errors? Kijk even hier, dat is gemakkelijker. 
                }
            }
          

        }

        public void DeleteTestData()
        {
            //Emtpy the table module and restraints
            using (var context = new DomainEntities())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudiePunten"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE FaseModules"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleLeerlijn"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Beoordelingen"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleTag"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleWerkvorm"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleCompetentie"); //r

                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Leerdoelen"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Leermiddelen"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudieBelasting"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudiePunten"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Weekplanning"); //r
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Voorkennis"); //r

                //many to many
                context.Database.ExecuteSqlCommand("DELETE FROM ModuleDocent");
                context.Database.ExecuteSqlCommand("DELETE FROM Docenten");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Docenten', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DELETE FROM Tag");
                context.Database.ExecuteSqlCommand("DELETE FROM Competentie");
                context.Database.ExecuteSqlCommand("DELETE FROM Leerlijn");
                context.Database.ExecuteSqlCommand("DELETE FROM Module");
     


                context.SaveChanges();
            }
        }
    }
}

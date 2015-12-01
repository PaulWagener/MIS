using ModuleManager.DomainDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModuleManager.WebTests.TestData
{
    public class TestData
    {
        public static DomainDAL.Module GetModule(DomainContext context)
        {

            var leerlijn = new Leerlijn() { Naam = "Programmeren", Schooljaar = "1516" };
            context.Leerlijn.Attach(leerlijn);

            return new DomainDAL.Module()
            {
                Naam = "Testing 101",
                Verantwoordelijke = "T. Est",
                CursusCode = "Test1",
                Schooljaar = "1516",
                Beschrijving = "Testing if software works",     
                Icon = "code",
                OnderdeelCode = "1",
                Status = "Nieuw",
                Leerlijn = new List<Leerlijn>(){leerlijn}
        
            };

        }

        public static void ClearTables(DomainContext context)
        {
            //Emtpy the table module and restraints

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudiePunten"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE FaseModules"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleLeerlijn"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Beoordelingen"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleTag"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleWerkvorm"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Docent"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Leerdoelen"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Leermiddelen"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudieBelasting"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE StudiePunten"); //r
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Weekplanning"); //r
            // unit.Context.Database.ExecuteSqlCommand("TRUNCATE TABLE ModuleCompetentie"); //r
            context.Database.ExecuteSqlCommand("DELETE FROM Module");

        }

        public static void DefaultTestData(DomainContext context)
        {
            ClearTables(context);

            context.Module.Add(GetModule(context));
            context.SaveChanges();
        }
    }
}

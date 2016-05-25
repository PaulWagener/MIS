namespace ModuleManager.DomainDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class DomainContext : DbContext
    {
        public DomainContext()
            : base("name=Domain")
        {
        }

        public DbSet<Beoordelingen> Beoordelingen { get; set; }
        public DbSet<Blok> Blokken { get; set; }
        public DbSet<Competentie> Competenties { get; set; }
        public DbSet<Docent> Docenten { get; set; }
        public DbSet<Fase> Fases { get; set; }
        public DbSet<FaseModules> FaseModules { get; set; }
        public DbSet<FaseType> FaseTypes { get; set; }
        public DbSet<Icons> Icons { get; set; }
        public DbSet<Leerdoelen> Leerdoelen { get; set; }
        public DbSet<Leerlijn> Leerlijn { get; set; }
        public DbSet<Leermiddelen> Leermiddelen { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleCompetentie> ModulesCompetentie { get; set; }
        public DbSet<ModuleWerkvorm> ModulesWerkvorm { get; set; }
        public DbSet<Niveau> Niveaux { get; set; }
        public DbSet<Onderdeel> Onderdelen { get; set; }
        public DbSet<Opleiding> Opleidingen { get; set; }
        public DbSet<Schooljaar> Schooljaren { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StudieBelasting> StudieBelasting { get; set; }
        public DbSet<StudiePunten> StudiePunten { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Toetsvorm> Toetsvormen { get; set; }
        public DbSet<Weekplanning> Weekplanning { get; set; }
        public DbSet<Werkvorm> Werkvormen { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModuleManager.DomainDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DomainContext : DbContext
    {
        public DomainContext()
            : base("name=DomainContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Competentie> Competentie { get; set; }
        public virtual DbSet<Fase> Fase { get; set; }
        public virtual DbSet<FaseModules> FaseModules { get; set; }
        public virtual DbSet<FaseType> FaseType { get; set; }
        public virtual DbSet<Leerlijn> Leerlijn { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleCompetentie> ModuleCompetentie { get; set; }
        public virtual DbSet<Niveau> Niveau { get; set; }
        public virtual DbSet<Opleiding> Opleiding { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StudieBelasting> StudieBelasting { get; set; }
        public virtual DbSet<StudiePunten> StudiePunten { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Weekplanning> Weekplanning { get; set; }
    }
}

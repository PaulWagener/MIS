using System.Collections.Generic;
using ModuleManager.DomainDAL;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class ModuleViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public string Beschrijving { get; set; }
        public string Naam { get; set; }
        public string Verantwoordelijke { get; set; }
        public string Status { get; set; }
        public string OnderdeelCode { get; set; }
        public string Icon { get; set; }

        public bool IsCompleted { get; set; }

        public IList<ModuleCompetentieViewModel> ModuleCompetentie { get; set; }
        public IList<StudiePuntenViewModel> StudiePunten { get; set; }
        public IList<FaseModulesViewModel> FaseModules { get; set; }
        public IList<StudieBelastingViewModel> StudieBelasting { get; set; }
        public IList<ModuleWerkvormViewModel> ModuleWerkvorm { get; set; }
        public IList<WeekplanningViewModel> Weekplanning { get; set; }
        public IList<BeoordelingenViewModel> Beoordelingen { get; set; }
        public IList<LeermiddelenViewModel> Leermiddelen { get; set; }
        public IList<LeerdoelenViewModel> Leerdoelen { get; set; }
        public IList<DocentViewModel> Docenten { get; set; }
        public IList<LeerlijnViewModel> Leerlijn { get; set; }
        public IList<TagViewModel> Tag { get; set; }
        public IList<ModuleVoorkennisViewModel> VoorkennisModules { get; set; }

        public ModuleViewModel()
        {
            //lege lijsten aanmaken
           ModuleCompetentie = new List<ModuleCompetentieViewModel>();
           StudiePunten = new List<StudiePuntenViewModel>();
           FaseModules = new List<FaseModulesViewModel>();
           StudieBelasting = new List<StudieBelastingViewModel>();
           ModuleWerkvorm = new List<ModuleWerkvormViewModel>();
           Weekplanning = new List<WeekplanningViewModel>();
           Beoordelingen = new List<BeoordelingenViewModel>();
           Leermiddelen = new List<LeermiddelenViewModel>();
           Leerdoelen = new List<LeerdoelenViewModel>();
           Docenten = new List<DocentViewModel>();
           Leerlijn = new List<LeerlijnViewModel>();
           Tag = new List<TagViewModel>();
           VoorkennisModules = new List<ModuleVoorkennisViewModel>();
        }

        public ICollection<ModuleCompetentie> MapToModuleCompetentie()
        {
            var moduleCompetenties = new List<ModuleCompetentie>();
            foreach (var moduleCompetentie in ModuleCompetentie)
            {
                moduleCompetenties.Add(new ModuleCompetentie
                {
                    CompetentieCode = moduleCompetentie.CompetentieCode,
                    CompetentieSchooljaar = moduleCompetentie.CompetentieSchooljaar,
                    CursusCode = moduleCompetentie.CursusCode,
                    Niveau = moduleCompetentie.Niveau,
                    Schooljaar = moduleCompetentie.Schooljaar
                });
            }
            return moduleCompetenties;
        }

        public ICollection<StudiePunten> MapToStudiePunten()
        {
            var studiePunten = new List<StudiePunten>();
            foreach (var studiepunt in StudiePunten)
            {
                studiePunten.Add(new StudiePunten
                {
                    CursusCode = studiepunt.CursusCode,
                    Schooljaar = studiepunt.Schooljaar,
                    ToetsCode = studiepunt.ToetsCode,
                    Toetsvorm = studiepunt.Toetsvorm,
                    EC = studiepunt.EC,
                    Minimum = studiepunt.Minimum
                });
            }
            return studiePunten;
        }

        public ICollection<FaseModules> MapToFaseModules(DomainContext context)
        {
            var faseModules = new List<FaseModules>();
            foreach (var faseModule in FaseModules)
            {
                var fase = context.Fase.First(m =>
                    m.Naam == faseModule.FaseNaam &&
                    m.Schooljaar == faseModule.FaseSchooljaar);

                faseModules.Add(new FaseModules
                {
                    Blok = faseModule.Blok,
                    FaseNaam = faseModule.FaseNaam,
                    FaseSchooljaar = faseModule.FaseSchooljaar,
                    ModuleCursusCode = faseModule.ModuleCursusCode,
                    ModuleSchooljaar = faseModule.ModuleSchooljaar,
                    OpleidingNaam = faseModule.OpleidingNaam,
                    OpleidingSchooljaar = faseModule.ModuleSchooljaar
                });
            }
            return faseModules;
        }

        public ICollection<StudieBelasting> MapToStudieBelasting()
        {
            var studieBelastings = new List<StudieBelasting>();
            foreach (var studieBelasting in StudieBelasting)
            {
                studieBelastings.Add(new StudieBelasting
                {
                    Activiteit = studieBelasting.Activiteit,
                    ContactUren = studieBelasting.ContactUren,

                    Duur = studieBelasting.Duur,
                    SBU = studieBelasting.SBU,
                    Frequentie = studieBelasting.Frequentie
                });
            }
            return studieBelastings;
        }

        public ICollection<ModuleWerkvorm> MapToModuleWerkvorm()
        {
            var moduleWerkvormen = new List<ModuleWerkvorm>();
            foreach (var moduleWerkvorm in ModuleWerkvorm)
            {
                moduleWerkvormen.Add(new ModuleWerkvorm
                {
                    CursusCode = moduleWerkvorm.CursusCode,
                    Organisatie = moduleWerkvorm.Organisatie,
                    Schooljaar = moduleWerkvorm.Schooljaar,
                    WerkvormType = moduleWerkvorm.WerkvormType
                });
            }
            return moduleWerkvormen;
        }

        public ICollection<Weekplanning> MapToWeekplanning()
        {
            var weekplanningen = new List<Weekplanning>();
            foreach (var weekplanning in Weekplanning)
            {
                weekplanningen.Add(new Weekplanning
                {
                    CursusCode = weekplanning.CursusCode,
                    Onderwerp = weekplanning.Onderwerp,
                    Schooljaar = weekplanning.Schooljaar,
                    Week = weekplanning.Week
                });
            }
            return weekplanningen;
        }

        public ICollection<Beoordelingen> MapToBeoordelingen()
        {
            var beoordelingen = new List<Beoordelingen>();
            foreach (var beoordeling in Beoordelingen)
            {
                beoordelingen.Add(new Beoordelingen
                {
                    Beschrijving = beoordeling.Beschrijving,
                    CursusCode = beoordeling.CursusCode,
                    Schooljaar = beoordeling.Schooljaar
                });
            }
            return beoordelingen;
        }

        public ICollection<Leermiddelen> MapToLeermiddelen()
        {
            var leermiddelen = new List<Leermiddelen>();
            foreach (var leermiddel in Leermiddelen)
            {
                leermiddelen.Add(new Leermiddelen
                {
                    Beschrijving = leermiddel.Beschrijving,
                    CursusCode = leermiddel.CursusCode,
                    Schooljaar = leermiddel.Schooljaar
                });
            }
            return leermiddelen;
        }

        //public ICollection<Leerdoelen> MapToLeerdoelen()
        //{
        //    var leerdoelen = new List<Leerdoelen>();
        //    foreach (var leerdoelVM in Leerdoelen)
        //    {
        //        var leerdoel = leerdoelVM.ToPoco();
        //        leerdoelen.Add(leerdoel);
                
        //    }
        //    return leerdoelen;
        //}

        public ICollection<Docent> MapToDocent()
        {
            var docenten = new List<Docent>();
            foreach (var docent in Docenten)
            {
                docenten.Add(new Docent
                {
                     Naam = docent.Naam,        
                });
            }
            return docenten;
        }

        public ICollection<Leerlijn> MapToLeerlijn(DomainContext context)
        {
            var leerlijnen = new List<Leerlijn>();
            foreach (var leerlijnVM in Leerlijn)
            {
                Leerlijn leerlijn = context.Leerlijn
                    .Find(new object[] { leerlijnVM.Naam, leerlijnVM.Schooljaar });

                leerlijn.Module = null;
               
                leerlijnen.Add(leerlijn);
            }
            return leerlijnen;
        }

        public ICollection<Tag> MapToTag(DomainContext context)
        {
            var tags = new List<Tag>();
            foreach (var tagVM in Tag)
            {
                Tag tag = context.Tag
                    .Find(new object[] { tagVM.Naam });

                tags.Add(tag);
            }
            return tags;
        }
    }
}
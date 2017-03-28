using System.Collections.Generic;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.PartialViewModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Mvc;
using ModuleManager.Web.Helpers;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class ModuleViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public string SchooljaarWeergave
        {
            get { return "20" + Schooljaar / 100 + "-20" + Schooljaar % 100; }
        }
        public string Beschrijving { get; set; }
        public string Naam { get; set; }
        public Docent Verantwoordelijke { get; set; }
        public string Status { get; set; }
        public string OnderdeelCode { get; set; }
        public string Icon { get; set; }
        public string Blok { get; set; }

        public bool Gecontroleerd { get; set; }

        public IList<StudiePuntenViewModel> StudiePunten { get; set; }
        public IList<StudieBelastingViewModel> StudieBelastingen { get; set; }
        public IList<ModuleWerkvormViewModel> ModuleWerkvormen { get; set; }
        public IList<WeekplanningViewModel> Weekplanningen { get; set; }
        public IList<BeoordelingenViewModel> Beoordelingen { get; set; }
        public IList<LeermiddelenViewModel> Leermiddelen { get; set; }
        public IList<LeerdoelenViewModel> Leerdoelen { get; set; }
        public IList<DocentViewModel> Docenten { get; set; }
        public IList<LeerlijnViewModel> Leerlijnen { get; set; }
        public IList<TagViewModel> Tags { get; set; }
        public IList<ModuleVoorkennisViewModel> Voorkennis { get; set; }
        public IList<Fase> Fases { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G29}")]
        public decimal StudieBelastingTotaal
        {
            get
            {
                return StudieBelastingen.Sum(sb => sb.SBU);
            }
        }

        public ModuleViewModel()
        {
            //lege lijsten aanmaken
            StudiePunten = new List<StudiePuntenViewModel>();
            StudieBelastingen = new List<StudieBelastingViewModel>();
            ModuleWerkvormen = new List<ModuleWerkvormViewModel>();
            Weekplanningen = new List<WeekplanningViewModel>();
            Beoordelingen = new List<BeoordelingenViewModel>();
            Leermiddelen = new List<LeermiddelenViewModel>();
            Leerdoelen = new List<LeerdoelenViewModel>();
            Docenten = new List<DocentViewModel>();
            Leerlijnen = new List<LeerlijnViewModel>();
            Tags = new List<TagViewModel>();
            Voorkennis = new List<ModuleVoorkennisViewModel>();
            Fases = new List<Fase>();
        }

        internal bool Validate(ModelStateDictionary modelState)
        {
            if (Leerlijnen.ContainsDoubles(ll => ll.Naam))
                modelState.AddModelError(String.Empty, "Leerlijnen mogen niet dubbel opgenomen worden.");
            if (Tags.ContainsDoubles(t => t.Naam))
                modelState.AddModelError(String.Empty, "Tags mogen niet dubbel opgenomen worden.");
            if (ModuleWerkvormen.ContainsDoubles(w => w.WerkvormType))
                modelState.AddModelError(String.Empty, "Werkvormen mogen niet dubbel opgenomen worden.");

            return modelState.IsValid;
        }
    }

}
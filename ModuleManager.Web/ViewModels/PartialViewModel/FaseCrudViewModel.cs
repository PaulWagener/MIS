using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System.ComponentModel.DataAnnotations;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class FaseCrudViewModel
    {
        public IEnumerable<FaseTypeViewModel> FaseTypes { get; set; }

        [Required]
        public string FaseType { get; set; }

        public string Opleiding { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Naam { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(512)]
        public string Beschrijving { get; set; }

        internal Fase ToPoco()
        {
            return new Fase()
            {
                Naam = this.Naam,
                Beschrijving = this.Beschrijving,
                FaseType = this.FaseType,
                OpleidingNaam = this.Opleiding,
                Modules = null,
            };
        }
    }
}
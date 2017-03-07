using System;
using ModuleManager.Domain;
using System.ComponentModel.DataAnnotations;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class CompetentieViewModel
    {

        [Required(ErrorMessage = "{0} dient ingevuld te worden")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} dient ingevuld te worden")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Naam { get; set; }

        [Required(ErrorMessage = "{0} dient ingevuld te worden")]
        [MinLength(3)]
        [MaxLength(512)]
        public string Beschrijving { get; set; }

        public CompetentieViewModel(Competentie competentie)
        {
            this.Code = competentie.Code;
            this.Naam = competentie.Naam;
            this.Beschrijving = competentie.Beschrijving;
        }

        public CompetentieViewModel()
        {
        }

        internal Competentie ToPoco()
        {
            return new Competentie()
            {
                Code = Code,
                Beschrijving = Beschrijving,
                Naam = Naam,
            };
        }
    }
}
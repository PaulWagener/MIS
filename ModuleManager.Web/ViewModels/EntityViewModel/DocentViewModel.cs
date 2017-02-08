using System;
using System.ComponentModel.DataAnnotations;
using ModuleManager.Domain;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class DocentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Docent dient ingevuld te worden")]
        [MinLength(2)]
        [MaxLength(128)]
        public string Naam { get; set; }

        public DocentViewModel(Docent docent)
        {
            this.Id = docent.Id;
            this.Naam = docent.Naam;
        }

        public DocentViewModel()
        {
        }

        internal Docent ToPoco()
        {
            return new Docent()
            {
                Id = this.Id,
                Naam = this.Naam,
            };
        }
    }
}
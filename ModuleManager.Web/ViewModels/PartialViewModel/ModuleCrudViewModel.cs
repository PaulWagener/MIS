using ModuleManager.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    public class ModuleCrudViewModel
    {
        public string OpleidingsPrefix { get; set; }
        public string CursusCode { get; set; }

        public string Schooljaar { get; set; }

        public string Naam { get; set; }
        public string Verantwoordelijke { get; set; }
        public string Blok { get; set; }
        public string Icon { get; set; }
        public string Onderdeel { get; set; }

        [Required]
        //[DisplayName("Toetscode")]
        public string Toetscode1Prefix { get; set; }
        public string Toetscode1 { get; set; }
        public string Toetscode2Prefix { get; set; }
        public string Toetscode2 { get; set; }

        [Required]
        public decimal Ec1 { get; set; }
        public decimal Ec2 { get; set; }

        public IEnumerable<Fase> Fases { get; set; }
        public IEnumerable<string> SelectedFases { get; set; }

        public IEnumerable<Blok> Blokken { get; set; }

        public IEnumerable<Icon> Icons { get; set; }

        [Display(Name = "Onderdeel")]
        public IEnumerable<Onderdeel> Onderdelen { get; set; }

        //public IEnumerable<StudiePunten> StudiePunten { get; set; }

        public ModuleCrudViewModel()
        {
            SelectedFases = new List<string>();
        }

    }

}
using ModuleManager.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerdoelenViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public int Id { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        public List<Competenties.KwaliteitskenmerkViewModel> Kwaliteitskenmerken { get; set; }

        public LeerdoelenViewModel()
        {
            Kwaliteitskenmerken = new List<Competenties.KwaliteitskenmerkViewModel>();
        }
    }
}
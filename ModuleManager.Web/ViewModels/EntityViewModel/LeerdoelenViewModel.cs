using ModuleManager.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerdoelenViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }
        public List<Competenties.KwaliteitskenmerkViewModel> Kwaliteitskenmerken { get; set; }

        public LeerdoelenViewModel()
        {
            Kwaliteitskenmerken = new List<Competenties.KwaliteitskenmerkViewModel>();
        }

        public Leerdoel ToPoco(DomainEntities context)
        {
            Leerdoel leerdoel = context.Leerdoelen.FirstOrDefault(l => l.CursusCode == CursusCode && l.Schooljaar == Schooljaar && l.Id == Id);

            if (leerdoel == null)
                leerdoel = new Leerdoel();

            leerdoel.Beschrijving = this.Beschrijving;
            leerdoel.CursusCode = this.CursusCode;
            leerdoel.Id = this.Id;
            leerdoel.Schooljaar = this.Schooljaar;

            leerdoel.Kwaliteitskenmerken.Clear();
            foreach (var kk in Kwaliteitskenmerken)
            {
                leerdoel.Kwaliteitskenmerken.Add(kk.ToPoCo());
            }

            return leerdoel;

        }
    }
}
using ModuleManager.Domain;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeermiddelenViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }

        public bool? isDeleted { get; set; }

        internal Leermiddel ToPoco(DomainEntities context)
        {
            Leermiddel leermiddel = context.Leermiddelen.FirstOrDefault(l => l.CursusCode == CursusCode && l.Schooljaar == Schooljaar && l.Id == Id);
            
            if (leermiddel == null)
                leermiddel = new Leermiddel();
          
            leermiddel.Beschrijving = this.Beschrijving;
            leermiddel.CursusCode = this.CursusCode;
            leermiddel.Id = this.Id;
            leermiddel.Schooljaar = this.Schooljaar;

            return leermiddel;
            
        }
    }
}
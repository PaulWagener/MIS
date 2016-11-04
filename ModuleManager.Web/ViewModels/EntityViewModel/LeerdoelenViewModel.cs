using ModuleManager.Domain;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerdoelenViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }

        public Leerdoel ToPoco(DomainEntities context)
        {
            Leerdoel leerdoel = context.Leerdoelen.FirstOrDefault(l => l.CursusCode == CursusCode && l.Schooljaar == Schooljaar && l.Id == Id);

            if (leerdoel == null)
                leerdoel = new Leerdoel();

            leerdoel.Beschrijving = this.Beschrijving;
            leerdoel.CursusCode = this.CursusCode;
            leerdoel.Id = this.Id;
            leerdoel.Schooljaar = this.Schooljaar;

            return leerdoel;

        }
    }
}
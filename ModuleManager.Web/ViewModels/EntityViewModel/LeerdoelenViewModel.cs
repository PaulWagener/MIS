using ModuleManager.DomainDAL;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerdoelenViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }

        public bool? isDeleted { get; set; }

        public Leerdoelen ToPoco(DomainContext context)
        {
            Leerdoelen leerdoel = context.Leerdoelen.FirstOrDefault(l => l.CursusCode == CursusCode && l.Schooljaar == Schooljaar && l.Id == Id);

            if (leerdoel == null)
                leerdoel = new Leerdoelen();

            leerdoel.Beschrijving = this.Beschrijving;
            leerdoel.CursusCode = this.CursusCode;
            leerdoel.Id = this.Id;
            leerdoel.Schooljaar = this.Schooljaar;

            return leerdoel;

        }
    }
}
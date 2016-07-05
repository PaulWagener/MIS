using ModuleManager.Domain;
using System.Linq;
namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class WeekplanningViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Week { get; set; }
        public string Onderwerp { get; set; }

        public bool? isDeleted { get; set; }

        internal Domain.Weekplanning ToPoco(DomainDalEntities context)
        {
            Domain.Weekplanning weekPlanning = context.Weekplanning.FirstOrDefault(w => w.CursusCode == CursusCode && w.Schooljaar == Schooljaar && w.Id == Id);
          // controleer eerst of beide velden gevuld zijn
          if (Week != null && Onderwerp != null) {

            if (weekPlanning == null)
            {
                weekPlanning = new Weekplanning();
            }
              weekPlanning.Id = Id;
              weekPlanning.Schooljaar = Schooljaar;
              weekPlanning.CursusCode = CursusCode;
              weekPlanning.Week = Week;
              weekPlanning.Onderwerp = Onderwerp;
            }
            return weekPlanning;
        }
    
    }
}
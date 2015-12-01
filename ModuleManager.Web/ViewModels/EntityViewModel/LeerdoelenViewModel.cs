using ModuleManager.DomainDAL;
namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerdoelenViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }

        public Leerdoelen ToPoco()
        {
            return new Leerdoelen
            {
                Beschrijving = this.Beschrijving,
                CursusCode = this.CursusCode,
                Schooljaar = this.Schooljaar
            };

        }


    }
}
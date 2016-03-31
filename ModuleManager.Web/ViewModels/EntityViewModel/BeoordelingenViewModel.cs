using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class BeoordelingenViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }

        public bool? isDeleted { get; set; }


        internal DomainDAL.Beoordelingen ToPoco(DomainDAL.DomainContext context)
        {
            DomainDAL.Beoordelingen beoordeling = context.Beoordelingen.FirstOrDefault(b => b.CursusCode == CursusCode && b.Schooljaar == Schooljaar && b.Id == Id);

            if (beoordeling == null)
                beoordeling = new DomainDAL.Beoordelingen();


            beoordeling.CursusCode = CursusCode;
            beoordeling.Schooljaar = this.Schooljaar;
            beoordeling.Beschrijving = Beschrijving;

            return beoordeling;
        } 
    }
}
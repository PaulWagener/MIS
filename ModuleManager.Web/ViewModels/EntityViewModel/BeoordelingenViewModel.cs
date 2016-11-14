using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class BeoordelingenViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }


        internal Domain.Beoordeling ToPoco(Domain.DomainEntities context)
        {
            Domain.Beoordeling beoordeling = context.Beoordelingen.FirstOrDefault(b => b.CursusCode == CursusCode && b.Schooljaar == Schooljaar && b.Id == Id);

            if (beoordeling == null)
                beoordeling = new Domain.Beoordeling();


            beoordeling.CursusCode = CursusCode;
            beoordeling.Schooljaar = this.Schooljaar;
            beoordeling.Beschrijving = Beschrijving;

            return beoordeling;
        } 
    }
}
using ModuleManager.DomainDAL;
using System;


namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class StudieBelastingViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public string Activiteit { get; set; }
        public Nullable<int> ContactUren { get; set; }
        public string Duur { get; set; }
        public string Frequentie { get; set; }
        public int SBU { get; set; }

        internal DomainDAL.StudieBelasting ToPoco(DomainContext context)
        {
            DomainDAL.StudieBelasting studieBelasting = context.StudieBelasting.Find(CursusCode, Schooljaar, Activiteit);

            if (studieBelasting == null)
            {
                studieBelasting = new StudieBelasting();
            }

            studieBelasting.CursusCode = CursusCode;
            studieBelasting.Schooljaar = Schooljaar;
            studieBelasting.Activiteit = Activiteit;
            studieBelasting.ContactUren = ContactUren;
            studieBelasting.Duur = Duur;
            studieBelasting.Frequentie = Frequentie;
            studieBelasting.SBU = SBU;

            return studieBelasting;
        }
    }
}
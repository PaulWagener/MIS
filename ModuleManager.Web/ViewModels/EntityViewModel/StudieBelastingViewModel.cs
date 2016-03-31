using ModuleManager.DomainDAL;
using System;
using System.ComponentModel.DataAnnotations;


namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class StudieBelastingViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }

        [Required(ErrorMessage="Verplicht")]
        public string Activiteit { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public Nullable<int> ContactUren { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public string Duur { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public string Frequentie { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public int SBU { get; set; }

        public bool? isDeleted { get; set; }


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
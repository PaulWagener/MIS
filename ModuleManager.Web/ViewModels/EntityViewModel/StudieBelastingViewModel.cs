using ModuleManager.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class StudieBelastingViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }

        [Required(ErrorMessage="Verplicht")]
        public string Activiteit { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G29}")]
        public decimal? ContactUren { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public string Duur { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        public string Frequentie { get; set; }

        [Required(ErrorMessage = "Verplicht")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G29}")]
        public decimal SBU { get; set; }

        internal Domain.StudieBelasting ToPoco(DomainEntities context)
        {
            Domain.StudieBelasting studieBelasting = context.StudieBelastings.FirstOrDefault(sb => sb.CursusCode == CursusCode && sb.Schooljaar == Schooljaar && sb.Activiteit == Activiteit);

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
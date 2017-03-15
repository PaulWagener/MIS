using ModuleManager.Domain;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class ModuleCompetentieViewModel
    {
        // TODO: Modulecompetenties vervangen door leerdoelen
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public string CompetentieCode { get; set; }
        public string CompetentieSchooljaar { get; set; }
        public string Niveau { get; set; }

        //public CompetentieViewModel Competentie { get; set; }

        // TODO: Goede representatie maken
        //internal ModuleCompetentie ToPoco(DomainEntities context, Module module)
        //{
        //    Domain.ModuleCompetentie moduleCompetentie = context.ModuleCompetenties.FirstOrDefault(l => 
        //        l.CursusCode == CursusCode &&
        //        l.Schooljaar == Schooljaar && 
        //        l.CompetentieCode == CompetentieCode);

        //    if (moduleCompetentie == null)
        //        moduleCompetentie = new Domain.ModuleCompetentie();


        //    moduleCompetentie.CursusCode = module.CursusCode;
        //    moduleCompetentie.Schooljaar = module.Schooljaar;
        //    moduleCompetentie.CompetentieCode = this.CompetentieCode;
        //    moduleCompetentie.Niveau = this.Niveau;

        //    return moduleCompetentie;
        //}
    }
}
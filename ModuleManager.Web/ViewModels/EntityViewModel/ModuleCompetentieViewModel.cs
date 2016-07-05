using ModuleManager.Domain;
using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class ModuleCompetentieViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public string CompetentieCode { get; set; }
        public string CompetentieSchooljaar { get; set; }
        public string Niveau { get; set; }

        public CompetentieViewModel Competentie { get; set; }

        internal ModuleCompetentie ToPoco(DomainDalEntities context, Module module)
        {
            Domain.ModuleCompetentie moduleCompetentie = context.ModuleCompetentie.
                FirstOrDefault(l => l.CursusCode == CursusCode &&
                l.Schooljaar == Schooljaar && 
                l.CompetentieCode == CompetentieCode);

            if (moduleCompetentie == null)
                moduleCompetentie = new Domain.ModuleCompetentie();


            moduleCompetentie.CursusCode = module.CursusCode;
            moduleCompetentie.Schooljaar = module.Schooljaar;
            moduleCompetentie.CompetentieCode = this.CompetentieCode;
            moduleCompetentie.Niveau = this.Niveau;
            moduleCompetentie.CompetentieSchooljaar = module.Schooljaar;

            return moduleCompetentie;
        }
    }
}
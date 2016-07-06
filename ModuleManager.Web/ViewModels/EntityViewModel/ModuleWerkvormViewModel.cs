using System.Linq;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class ModuleWerkvormViewModel
    {
        public string CursusCode { get; set; }
        public string Schooljaar { get; set; }
        public string WerkvormType { get; set; }
        public string Organisatie { get; set; }

        public Domain.ModuleWerkvorm ToPoco(Domain.DomainEntities context)
        {
            Domain.ModuleWerkvorm moduleWerkvorm = context.ModuleWerkvormen.FirstOrDefault(l => l.CursusCode == CursusCode && 
                l.Schooljaar == Schooljaar && l.WerkvormType == WerkvormType);

            if (moduleWerkvorm == null)
                moduleWerkvorm = new Domain.ModuleWerkvorm();


            moduleWerkvorm.CursusCode = CursusCode;
            moduleWerkvorm.Schooljaar = this.Schooljaar;
            moduleWerkvorm.WerkvormType = WerkvormType;
            moduleWerkvorm.Organisatie = Organisatie;

            return moduleWerkvorm;
        }
    }
}
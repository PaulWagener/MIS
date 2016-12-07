using System.Collections.Generic;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    // één sectie van lessentabellen
    public class LessenTabelViewModel
    {
        public LessenTabelViewModel()
        {
            Tabellen = new List<LesTabelViewModel>();
        }

        public string FaseType { get; set; }
        public string FaseNaam { get; set; }

        public ICollection<LesTabelViewModel> Tabellen { get; set; }
    }
}
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System.Web.Mvc;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class ModuleEditOptionsViewModel
    {
        public ICollection<TagViewModel> Tags { get; set; }

        public MultiSelectList Competenties { get; set; }
        //public ICollection<CompetentieViewModel> Competenties { get; set; }

        public ICollection<LeerlijnViewModel> Leerlijnen { get; set; }
        public ICollection<WerkvormViewModel> Werkvormen { get; set; }
        public ICollection<ToetsvormViewModel> Toetsvormen { get; set; }
        public ICollection<ModuleVoorkennisViewModel> VoorkennisModules { get; set; }
        public ICollection<NiveauViewModel> Niveaus { get; set; }
        public ICollection<DocentViewModel> Docenten { get; set; }
        public void Filter(ModuleViewModel vm)
        {

        }
    }
}
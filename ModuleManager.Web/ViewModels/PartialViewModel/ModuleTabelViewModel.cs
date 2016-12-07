using ModuleManager.Domain;
using System.Collections.Generic;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    // één row in de lessentabel
    public class ModuleTabelViewModel
    {
        public string Cursuscode { get; set; }
        public string Omschrijving { get; set; }
        public IList<int> Contacturen { get; set; }
        public IList<string> Werkvormen { get; set; }
        public IList<StudiePunt> Studiepunten { get; set; }
    }
}
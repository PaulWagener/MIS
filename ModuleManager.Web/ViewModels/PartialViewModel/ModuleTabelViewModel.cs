using ModuleManager.Domain;
using System.Collections.Generic;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    // één row in de lessentabel
    public class ModuleTabelViewModel
    {
        public string Onderdeel { get; set; }
        public string Cursuscode { get; set; }
        public string Omschrijving { get; set; }
        public string Contacturen { get; set; }
        public string Werkvormen { get; set; }
        public ICollection<StudiePunt> Studiepunten { get; set; }
    }
}
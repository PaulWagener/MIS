namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    /// <summary>
    /// Moduleinformatie weergegeven voor de module overview-pagina's
    /// </summary>
    public class ModulePartialViewModel
    {
        /// <summary>
        /// Onderdeel van de Module/Overview
        /// </summary>
        public string Icon { get; set; }
        public string Naam { get; set; }
        public string CursusCode { get; set; }
        public string Blokken { get; set; }
        public decimal TotalEc { get; set; }
        public int Schooljaar { get; set; }

        /// <summary>
        /// Onderdeel van de Admin/CheckModules
        /// </summary>
        public string Status { get; set; }
        public string Verantwoordelijke { get; set; }
        public string Docenten { get; set; }

        /// <summary>
        /// Onderdeel van de Admin/Curriculum
        /// </summary>
        public string FaseNamen { get; set; }
    }
}
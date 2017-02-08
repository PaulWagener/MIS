using System.Collections.Generic;

namespace ModuleManager.Web.ViewModels.RequestViewModels
{
    public class Filter
    {
        public Filter()
        {
            this.Competenties = new List<string>();
            this.Leerlijnen = new List<string>();
            this.Fases = new List<string>();
            this.Blokken = new List<int>();
            this.Tags = new List<string>();
        }

        public ICollection<string> Competenties { get; set; }
        public ICollection<string> Leerlijnen { get; set; }
        public ICollection<string> Fases { get; set; }
        public ICollection<int> Blokken { get; set; }
        public ICollection<string> Tags { get; set; }
        public string Zoekterm { get; set; }
        public int? Leerjaar { get; set; }
        public string Ec { get; set; }
    }
}
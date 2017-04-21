using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.ViewModels.RequestViewModels
{
    public class Export
    {
        public bool CursusCode { get; set; }
        public bool Naam { get; set; }
        public bool Beschrijving { get; set; }
        public bool AlgemeneInformatie { get; set; }
        public bool Studiebelasting { get; set; }
        public bool Organisatie { get; set; }
        public bool Weekplanning { get; set; }
        public bool Beoordeling { get; set; }
        public bool Leermiddelen { get; set; }
        public bool Leerdoelen { get; set; }
        public bool Leerlijnen { get; set; }
        public bool Tags { get; set; }
    }
}
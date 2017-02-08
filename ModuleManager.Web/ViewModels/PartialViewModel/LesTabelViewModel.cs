using System;
using System.Collections.Generic;
using System.Linq;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    // één lessentabel
    public class LesTabelViewModel
    {
        public LesTabelViewModel()
        {
            Onderdelen = new List<OnderdeelTabelViewModel>();
        }

        public int Blok { get; set; }

        public string Semester
        {
            get
            {
                return "" + Math.Ceiling(Convert.ToInt32(Blok) / 2.0);
            }
        }

        public int TotaleContactUren
        {
            get
            {
                return Onderdelen.SelectMany(src => src.Modules).Sum(m => m.Contacturen.Sum());
            }
        }

        public int TotaleEcs
        {
            get
            {
                return (int)Onderdelen.SelectMany(src => src.Modules).Sum(m => m.Studiepunten.Sum(sp => sp.EC));
            }
        }

        public IList<OnderdeelTabelViewModel> Onderdelen { get; set; }
    }
}
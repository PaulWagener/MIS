using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.ViewModels.EntityViewModel.Competenties
{
    public class CompetentieViewModel
    {
        public int Volgnummer { get; set; }
        public int? Id { get; set; }
        public string Naam { get; set; }

        public List<CompetentieOnderdeelViewModel> Onderdelen { get; set; }

        public CompetentieViewModel()
        {
            Onderdelen = new List<CompetentieOnderdeelViewModel>();
        }

        public Competentie ToPoCo()
        {
            var poco = new Competentie()
            {
                Naam = Naam,
                Volgnummer = Volgnummer,
            };

            if (Id.HasValue) poco.Id = Id.Value;

            Onderdelen.ForEach(o => poco.CompetentieOnderdelen.Add(o.ToPoCo()));

            return poco;
        }
    }
}
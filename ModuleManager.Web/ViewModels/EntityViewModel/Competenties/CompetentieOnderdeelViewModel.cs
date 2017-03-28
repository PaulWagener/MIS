using System;
using System.Collections.Generic;
using ModuleManager.Domain;

namespace ModuleManager.Web.ViewModels.EntityViewModel.Competenties
{
    public class CompetentieOnderdeelViewModel
    {
        public int? Id { get; set; }
        public int Volgnummer { get; set; }
        public string Naam { get; set; }
        public string Code { get; set; }

        public List<KwaliteitskenmerkViewModel> Kwaliteitskenmerken { get; set; }

        public CompetentieOnderdeelViewModel()
        {
            Kwaliteitskenmerken = new List<KwaliteitskenmerkViewModel>();
        }

        internal CompetentieOnderdeel ToPoCo()
        {
            var poco = new CompetentieOnderdeel()
            {
                Naam = Naam,
                Volgnummer = Volgnummer,
            };

            if (Id.HasValue) poco.Id = Id.Value;

            Kwaliteitskenmerken.ForEach(kk => poco.Kwaliteitskenmerken.Add(kk.ToPoCo()));

            return poco;
        }
    }
}
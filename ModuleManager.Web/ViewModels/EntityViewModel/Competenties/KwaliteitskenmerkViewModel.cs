using System;
using ModuleManager.Domain;

namespace ModuleManager.Web.ViewModels.EntityViewModel.Competenties
{
    public class KwaliteitskenmerkViewModel
    {
        public int? Id { get; set; }
        public int Volgnummer { get; set; }
        public string Omschrijving { get; set; }
        public string Code => $"K{Volgnummer}";

        internal Kwaliteitskenmerk ToPoCo()
        {
            var poco = new Kwaliteitskenmerk()
            {
                Omschrijving = Omschrijving,
                Volgnummer = Volgnummer,
            };

            if (Id.HasValue) poco.Id = Id.Value;

            return poco;
        }
    }
}
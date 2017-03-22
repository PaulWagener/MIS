using ModuleManager.BusinessLogic.Data;
using System.Collections.Generic;
using System.Linq;

namespace ModuleManager.Web.ViewModels.RequestViewModels
{
    public class ArgumentsViewModel
    {
        public OrderBy OrderBy { get; set; }
        public Filter Filter { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }

        public ModuleFilterSorterArguments ToModuleFilterSorterArguments()
        {
            ModuleFilterSorterArguments args = new ModuleFilterSorterArguments()
            {
                Limit = Length ?? 10,
                Offset = Start ?? 0
            };

            if (Filter.Competenties.Any(item => item != null)) args.CompetentieFilters = Filter.Competenties;
            if (Filter.Tags.Any(item => item != null)) args.TagFilters = Filter.Tags;
            if (Filter.Leerlijnen.Any(item => item != null)) args.LeerlijnFilters = Filter.Leerlijnen;
            if (Filter.Fases.Any(item => item != null)) args.FaseFilters = Filter.Fases;
            if (Filter.Blokken.First() > 0) args.BlokFilters = Filter.Blokken.ToArray();
            if (Filter.Zoekterm != null) args.ZoektermFilter = Filter.Zoekterm;
            if (Filter.Leerjaar.HasValue) args.LeerjaarFilter = Filter.Leerjaar;

            switch (OrderBy.Column)
            {
                case 1: args.SortBy = "Naam"; break;
                case 2: args.SortBy = "CursusCode"; break;
                case 3: args.SortBy = "Schooljaar"; break;
                case 7: args.SortBy = "Verantwoordelijke"; break;
                default: args.SortBy = "Naam";  break;
            }

            args.SortDesc = OrderBy.Dir == "desc";

            return args;
        }
    }
}
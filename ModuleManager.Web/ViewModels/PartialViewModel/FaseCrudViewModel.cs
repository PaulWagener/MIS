using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.EntityViewModel;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class FaseCrudViewModel
    {

        public IEnumerable<FaseTypeViewModel> FaseTypes { get; set; }

        public FaseTypeViewModel FaseType { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }

    }
}
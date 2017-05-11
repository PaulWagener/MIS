using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModuleManager.Domain;

namespace ModuleManager.Web.ViewModels
{
    public class PortalVM
    {
        public List<Module> ModulesContributed { get; internal set; }
        public List<Module> ModulesOwned { get; internal set; }
        public int Schooljaar { get; internal set; }
        public Domain.User User { get; set; }
    }
}
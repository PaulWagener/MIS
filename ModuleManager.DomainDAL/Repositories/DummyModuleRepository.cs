﻿using System.Collections.Generic;
using System.Linq;
using ModuleManager.DomainDAL.RepositoryInterfaces;

namespace ModuleManager.DomainDAL.Repositories
{
    public class DummyModuleRepository : IModuleRepository
    {
        private readonly List<Module> _modules;

        public DummyModuleRepository()
        {
            _modules = new List<Module>
            {
                new Module
                {
                    CursusCode = "IIIN-DATAB3",
                    Naam = "2-AII1415-Databases 3"
                },
                new Module
                {
                    CursusCode = "IIIN-PROG6",
                    Naam = "2-AII1415-Programmeren 6 - C Sharp"
                },
                new Module
                {
                    CursusCode = "IIIN-ALG",
                    Naam = "1-AII1415-Algoritmiek en Datastructuren"
                }
            };
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _modules;
        }

        public Module GetModule(string cursusCode)
        {
            return _modules.FirstOrDefault(module => module.CursusCode.Equals(cursusCode));
        }
    }
}
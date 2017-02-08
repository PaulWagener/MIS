using ModuleManager.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ModuleManager.Web.Helpers;
using ModuleManager.Domain.Interfaces;
using System;
using System.Data.Entity;


namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    public class ModuleCrudViewModel : IValidatableObject
    {
        public int Schooljaar { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string CursusCode { get; set; }

        public string OpleidingsPrefix { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string Naam { get; set; }

        [Required(ErrorMessage = "The verantwoordelijke field is required")]
        public int VerantwoordelijkeDocentId { get; set; }

        [Required]
        public int Blok { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Onderdeel { get; set; }

        public string Toetscode1Prefix { get; set; }

        [Required]
        public string Toetscode1 { get; set; }

        public string Toetscode2Prefix { get; set; }

        public string Toetscode2 { get; set; }

        [Required]
        public double Ec1 { get; set; }
        public double Ec2 { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<string> SelectedFases { get; set; }



        /** select option data **/

        public IEnumerable<Fase> Fases { get; set; }

        public IEnumerable<Blok> Blokken { get; set; }

        public IEnumerable<Docent> Docenten { get; set; }

        public IEnumerable<Icon> Icons { get; set; }

        [Display(Name = "Onderdeel")]
        public IEnumerable<Onderdeel> Onderdelen { get; set; }

        //public IEnumerable<StudiePunten> StudiePunten { get; set; }

        public ModuleCrudViewModel()
        {

        }

        public ModuleCrudViewModel(IUnitOfWork unitOfWork)
        {
            var schooljaren = unitOfWork.GetRepository<Schooljaar>().GetAll().ToArray();
            var schooljaar = schooljaren.Last();
            Schooljaar = schooljaar.JaarId;
            OpleidingsPrefix = "IIIN";
            this.SetOptions(unitOfWork);        
        }


        public ModuleCrudViewModel(Module module, IUnitOfWork unitOfWork)
        {
            CursusCode = module.CursusCode;
            Naam = module.Naam;
            Icon = module.Icon;
            VerantwoordelijkeDocentId = module.Verantwoordelijke.Id;
            Onderdeel = module.Onderdeel.Code;
            Toetscode1 = module.StudiePunten.Count > 0 ? module.StudiePunten.ElementAt(0).ToetsCode : null;
            Ec1 = module.StudiePunten.Count > 0 ? module.StudiePunten.ElementAt(0).EC : 0;
            Toetscode2 = module.StudiePunten.Count > 1 ? module.StudiePunten.ElementAt(1).ToetsCode : null;
            Ec2 = module.StudiePunten.Count > 1 ? module.StudiePunten.ElementAt(1).EC : 0;
            SelectedFases = module.Fases.Select(p => p.Naam);
            SetOptions(unitOfWork);
        }

        public void SetOptions(IUnitOfWork unitOfWork)
        {
            Fases = unitOfWork.GetRepository<Fase>().GetAll().ToList();
            Icons = unitOfWork.GetRepository<Icon>().GetAll().ToList();
            Onderdelen = unitOfWork.GetRepository<Onderdeel>().GetAll().ToList();
            Docenten = unitOfWork.GetRepository<Docent>().GetAll().ToList();
            Blokken = unitOfWork.GetRepository<Blok>()
                .GetAll()
                .ToList()
                .OrderBy(B => B.BlokId);
        }


        internal Module ToPoco(IUnitOfWork unitOfWork)
        {
            ICollection<StudiePunt> studiepuntenList = new List<StudiePunt>();

            var studiepunt1 = new StudiePunt()
            {
                ToetsCode = Toetscode1Prefix != null ? String.Format("{0}-{1}", this.Toetscode1Prefix, this.Toetscode1) : this.Toetscode1,
                EC = this.Ec1
            };
            studiepuntenList.Add(studiepunt1);

            if (Toetscode2 != null || Toetscode1 == Toetscode2)
            {
                var studiepunt2 = new StudiePunt()
                {
                    ToetsCode = Toetscode1Prefix != null ? String.Format("{0}-{1}", this.Toetscode2Prefix, this.Toetscode2) : this.Toetscode2,
                    EC = this.Ec2
                };
                studiepuntenList.Add(studiepunt2);
            }

            ICollection<Fase> fases = unitOfWork.Context.Fases.Where(f => this.SelectedFases.Contains(f.Naam)).ToList();

            var module = unitOfWork.Context.Modules
                .Include(m => m.Fases)
                .Include(m => m.Onderdeel)
                .Include(m => m.StudiePunten)
                .Include(m => m.Verantwoordelijke)
                .FirstOrDefault(m => m.CursusCode == CursusCode && m.Schooljaar == Schooljaar);
                            
            if(module != null)
            {
                //load navigation properties
                var entry = unitOfWork.Context.Entry(module);
                entry.Collection("Fases").Load();
            }
            else
            {
                //make new module
                module = module != null ? module : new Module();
            }

            module.Schooljaar = this.Schooljaar;
            module.StudiePunten = studiepuntenList;
            module.Blok = this.Blok;
            module.Naam = this.Naam;
            module.Icon = this.Icon;
            module.Status = "Nieuw";
            module.Gecontroleerd = false;
            module.VerantwoordelijkeDocentId = this.VerantwoordelijkeDocentId;
            module.OnderdeelCode = this.Onderdeel;
            module.Fases = fases;
            module.CursusCode = this.OpleidingsPrefix != null
                                 ? String.Format("{0}-{1}", this.OpleidingsPrefix, this.CursusCode)
                                 : this.CursusCode;

            return module;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if(Toetscode1 != null && Ec1 == 0)
                result.Add(new ValidationResult("Aantal EC is verplicht indien toetscode is ingevuld ", new[] { "Toetscode1", "Ec1" }));

            if (Toetscode2 != null && Ec2 == 0)
                result.Add(new ValidationResult("Aantal EC is verplicht indien toetscode is ingevuld ", new[] { "Toetscode2", "Ec2" }));

            return result;
        }
    }

}
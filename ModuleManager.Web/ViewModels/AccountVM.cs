using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserNaam { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }    
        [DisplayName("Onthoud mij")]
        public bool Remember { get; set; }

    }

    public class RegistrationVM
    {
        [Required]
        [DisplayName("Gebruikersnaam")]
        public string UserNaam { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Wachtwoord")]
        public string Wachtwoord { get; set; }

        [Compare("Wachtwoord")]
        [DataType(DataType.Password)]
        [DisplayName("Bevestig wachtwoord")]
        public string BevestigWachtwoord { get; set; }
        [Required]
        [DisplayName("Systeemrol")]
        public string SelectedSysteemRol { get; set; }

        public virtual IEnumerable<SysteemRol> SysteemRollen { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Naam { get; set; }


    }
}
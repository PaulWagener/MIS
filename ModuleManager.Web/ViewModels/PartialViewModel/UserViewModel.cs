using ModuleManager.Domain;
using ModuleManager.UserDAL;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ModuleManager.Web.ViewModels.PartialViewModel
{

    /// <summary>
    /// Onderdeel van alle views behalve de Home/Login-View & de Home/Index-View
    /// </summary>
    public class UserViewModel
    {
        public string Naam { get; set; }

        public string GebruikersNaam { get; set; }

        public string SysteemRol { get; set; }

        /// <summary>
        /// Onderdeel van de Admin/Usermanagement-View
        /// Bevat extra informatie die moet worden weergegeven bij de gebruikers-management
        /// </summary>
        public string Email { get; set; }

        public bool Blocked { get; set; }
    }

    public class UserEditViewModel
    {
        [Required]
        [DisplayName("Gebruikersnaam")]
        [StringLength(255, MinimumLength = 3)]
        public string UserNaam { get; set; }
        public string OudeUserNaam { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        [DisplayName("Systeemrol")]
        [Required]
        public string SysteemRol { get; set; }

        public virtual IEnumerable<SysteemRol> SysteemRollen { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Naam { get; set; }

        public bool Blocked { get; set; }
    }

    public class UserLockViewModel
    {

        public string UserNaam { get; set; }
        public string Wachtwoord { get; set; }      
        public string SysteemRol { get; set; }    
        public string Email { get; set; } 
        public string Naam { get; set; }
        public bool Blocked { get; set; }
    }
}
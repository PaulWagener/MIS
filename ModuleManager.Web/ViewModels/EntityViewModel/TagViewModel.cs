using System;
using ModuleManager.Domain;
using System.ComponentModel.DataAnnotations;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class TagViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Naam { get; set; }

        internal Tag ToPoco()
        {
            return new Tag()
            {
                Naam = this.Naam,
            };
        }
    }
}
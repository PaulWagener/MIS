using ModuleManager.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class OnderdeelViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Code { get; set; }

        internal Onderdeel ToPoco()
        {
            return new Onderdeel()
            {
                Code = this.Code,
            };
        }
    }
}
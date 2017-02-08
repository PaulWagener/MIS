using ModuleManager.Domain;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerlijnViewModel
    {
        [Required(ErrorMessage = "Leerlijn dient ingevuld te worden")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Naam { get; set; }

        internal Domain.Leerlijn ToPoco()
        {
            return new Domain.Leerlijn()
            {
                Naam = this.Naam
            };
        }
    }
}
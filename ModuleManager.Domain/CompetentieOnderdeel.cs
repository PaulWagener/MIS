//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModuleManager.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompetentieOnderdeel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompetentieOnderdeel()
        {
            this.Kwaliteitskenmerken = new HashSet<Kwaliteitskenmerk>();
        }
    
        public int Id { get; set; }
        public int CompetentieId { get; set; }
        public int Volgnummer { get; set; }
        public string Naam { get; set; }
    
        public virtual Competentie Competentie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kwaliteitskenmerk> Kwaliteitskenmerken { get; set; }
    }
}

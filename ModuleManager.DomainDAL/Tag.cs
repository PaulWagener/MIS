//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModuleManager.DomainDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag
    {
        public Tag()
        {
            this.Module = new HashSet<Module>();
        }
    
        public string Naam { get; set; }
        public System.DateTime Schooljaar { get; set; }
    
        public virtual ICollection<Module> Module { get; set; }
    }
}

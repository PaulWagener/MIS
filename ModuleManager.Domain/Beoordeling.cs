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
    
    public partial class Beoordeling
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public int Id { get; set; }
        public string Beschrijving { get; set; }
    
        public virtual Module Module { get; set; }
    }
}

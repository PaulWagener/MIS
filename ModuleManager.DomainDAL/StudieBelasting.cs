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
    
    public partial class StudieBelasting
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public int Id { get; set; }
        public string Activiteit { get; set; }
        public string Duur { get; set; }
        public string Frequentie { get; set; }
        public int SBU { get; set; }
    
        public virtual Module Module { get; set; }
    }
}
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
    
    public partial class StudiePunt
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public string ToetsCode { get; set; }
        public string Toetsvorm { get; set; }
        public double EC { get; set; }
        public string Minimum { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Toetsvorm Toetsvorm1 { get; set; }
    }
}

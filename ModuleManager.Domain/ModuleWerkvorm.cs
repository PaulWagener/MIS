
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
    
public partial class ModuleWerkvorm
{

    public string CursusCode { get; set; }

    public string Schooljaar { get; set; }

    public string WerkvormType { get; set; }

    public string Organisatie { get; set; }



    public virtual Module Module { get; set; }

    public virtual Werkvorm Werkvorm { get; set; }

}

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.ViewModels
{
    public class AdminArchiverenViewModel
    {
        [Display(Name="Weet uw zeker dat uw het huidige cohort wil achiveren? Zoja Type dan \"ARCHIVEREN\" in hoofdletters hieronder.")]
        public string BevestigingsCode { get; set; }

        [Display(Name = "Wilt u het cohort kopiëren naar een nieuw cohort?")]
        public bool CopyCohort { get; set; }

        [Display(Name = "Kopieer naar nieuw cohort")]
        public int? CopyToCohort { get; set; }

        [Display(Name = "Te archiveren cohort")]
        public int TeArchiverenCohort { get; set; }

        public int[] Cohorten { get; set; }
    }
}
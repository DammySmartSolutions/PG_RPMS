//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CorrectRPMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblGrading
    {
        public int GradingID { get; set; }
        public string GradingSystemType { get; set; }
        public int GradingSystemTypeID { get; set; }
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public decimal GradePoint { get; set; }
        public string GradeLetter { get; set; }
        public bool GradePassed { get; set; }
    }
}

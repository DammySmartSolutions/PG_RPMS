using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupGrading
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
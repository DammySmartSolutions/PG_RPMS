using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupStudentCourseExempt
    {
        public int StudCourseExemptID { get; set; }
        public string MatricNumber { get; set; }
        public string CourseCode { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }


    }
}
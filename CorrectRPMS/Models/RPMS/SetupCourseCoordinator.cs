using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupCourseCoordinator
    {

        public int AssignCourseID { get; set; }
        public string StaffNumber { get; set; }
        public string FacultyCode { get; set; }
        public string DeptCode { get; set; }
        public string Course { get; set; }
        public string SemesterSession { get; set; }
    }
}
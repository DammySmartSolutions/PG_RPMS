using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupCourses
    {
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int DeptID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseID { get; set; }
    }
}
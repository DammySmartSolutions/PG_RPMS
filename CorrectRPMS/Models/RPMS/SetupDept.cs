using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupDept
    {
        public int DeptID { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public string FacultyType { get; set; }

        public int FacultyID { get; set; }
    }

}

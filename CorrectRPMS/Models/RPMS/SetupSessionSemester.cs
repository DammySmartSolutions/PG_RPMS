using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupSessionSemester
    {
        public int SessionSemesterID { get; set; }
        public string SessionSemester { get; set; }

        public string Session { get; set; }
        public string Semester { get; set; }
    }
}
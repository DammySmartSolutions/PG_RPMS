using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupStudentDiscLeave
    {

        public int DiscLeaveID { get; set; }
        public string MatricNumber { get; set; }
        public string Status { get; set; }
        public string SessSemester { get; set; }
        public int Duration { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}
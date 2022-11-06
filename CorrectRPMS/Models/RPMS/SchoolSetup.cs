using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SchoolSetup
    {
        public int SchoolID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
    }
}
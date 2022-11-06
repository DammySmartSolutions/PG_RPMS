using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupUser
    {
        public int StaffID { get; set; }
        public string StaffNumber { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Mobile { get; set; }
        public string Sex { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool UpdatePassword { get; set; }

        public string Faculty { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LinkExpirationDate { get; set; }
        public string CreatedBy { get; set; }
        public bool LinkActivated { get; set; }

        public bool SecurityQueCreated { get; set; }



    }
}
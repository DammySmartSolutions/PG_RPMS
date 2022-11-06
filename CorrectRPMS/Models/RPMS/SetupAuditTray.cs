using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupAuditTray
    {

        public int AuditID { get; set; }
        public string StaffNumber { get; set; }
        public string ActionPerformed { get; set; }
        public System.DateTime DatePerformed { get; set; }
        public string Desccription { get; set; }
    }
}
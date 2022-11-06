using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupLocalGovt
    {

        public int LocalGovtID { get; set; }
        public int StateID { get; set; }
        public string LocalGovt { get; set; }
        public string State { get; set; }
        public string NationName { get; set; }
    }
}
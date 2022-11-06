using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupStates
    {
        public int StateId { get; set; }
        public int NationId { get; set; }
        public string NationCode { get; set; }
        public string Statename { get; set; }
        public string NationName { get; set; }
    }
}
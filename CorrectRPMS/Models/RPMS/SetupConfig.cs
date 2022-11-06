using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupConfig
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailHost { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool BodyHtml { get; set; }
        public int ConfigID { get; set; }
    }
}
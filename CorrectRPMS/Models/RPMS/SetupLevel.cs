using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorrectRPMS.Models.RPMS
{
    public class SetupLevel
    {

        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public string FullDescription { get; set; }
        public int LevelOrder { get; set; }
        public bool IsAbsent { get; set; }
        public bool WillReturn { get; set; }
    }
}
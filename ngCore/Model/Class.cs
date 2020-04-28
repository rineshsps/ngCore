using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngCore.Model
{

    public class Rootobject
    {
        public Appsettings AppSettings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Appsettings
    {
        public string Key { get; set; }
        public string name { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
    }

}

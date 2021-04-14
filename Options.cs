using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace mini_webserver
{
    public class Options
    {
        [Option('v', "vpath", Required = false, HelpText = "VitualPath",
            ResourceType = typeof(Properties.Resource))]
        public string VitualPath { get; set; }

        [Value(0, MetaName = "InputDir",
            HelpText = "HelpTextFileName",
            Required = false,
            ResourceType = typeof(Properties.Resource))]
        public string DirName { get; set; }
    }
}

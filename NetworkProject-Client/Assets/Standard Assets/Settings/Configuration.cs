using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    [Serializable]
    public class Configuration
    {
        public string DefaultLanguageName { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public Configuration()
        {
            DefaultLanguageName = "";
            IP = "";
        }
    }
}

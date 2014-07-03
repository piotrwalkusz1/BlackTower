using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class Jump : INetworkPackage
    {
        public int IdNet { get; set; }

        public Jump(int idNet)
        {
            IdNet = idNet;
        }
    }
}

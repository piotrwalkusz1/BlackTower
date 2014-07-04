using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class Dead : INetworkPackage
    {
        public int IdNet { get; set; }

        public Dead(int idNet)
        {
            IdNet = idNet;
        }
    }
}

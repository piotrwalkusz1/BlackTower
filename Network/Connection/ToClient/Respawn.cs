using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class Respawn : INetworkPackage
    {
        public int IdNet { get; set; }

        public Respawn(int idNet)
        {
            IdNet = idNet;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class Attack : INetworkPackage
    {
        public int IdNet { get; set; }

        public Attack(int idNet)
        {
            IdNet = idNet;
        }
    }
}

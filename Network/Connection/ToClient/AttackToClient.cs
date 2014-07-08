using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class AttackToClient : INetworkRequest
    {
        public int IdNet { get; set; }

        public AttackToClient(int idNet)
        {
            IdNet = idNet;
        }
    }
}

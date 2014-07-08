using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class RespawnToClient : INetworkRequest
    {
        public int IdNet { get; set; }

        public RespawnToClient(int idNet)
        {
            IdNet = idNet;
        }
    }
}

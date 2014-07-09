using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class DeadToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }

        public DeadToClient(int idNet)
        {
            IdNet = idNet;
        }
    }
}

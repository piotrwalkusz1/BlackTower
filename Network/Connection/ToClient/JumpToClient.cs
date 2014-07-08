using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class JumpToClient : INetworkRequest
    {
        public int IdNet { get; set; }

        public JumpToClient(int idNet)
        {
            IdNet = idNet;
        }
    }
}

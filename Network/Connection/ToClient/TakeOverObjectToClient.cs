using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class TakeOverObjectToClient : INetworkRequestToClient
    {
        public int NetId { get; set; }

        public TakeOverObjectToClient(int netId)
        {
            NetId = netId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateGoldToClient : INetworkRequestToClient
    {
        public int NetId { get; set; }
        public int Gold { get; set; }

        public UpdateGoldToClient(int netId, int gold)
        {
            NetId = netId;
            Gold = gold;
        }
    }
}

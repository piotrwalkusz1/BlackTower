using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ExecuteActionToServer : INetworkRequestToServer
    {
        public int NetId { get; set; }
        public int ActionId { get; set; }

        public ExecuteActionToServer(int netId, int actionId)
        {
            NetId = netId;
            ActionId = actionId;
        }
    }
}

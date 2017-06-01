using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ChangeMoveTypeToClient : INetworkRequestToClient
    {
        public int NetId { get; set; }
        public int MoveType { get; set; }

        public ChangeMoveTypeToClient(int netId, int moveType)
        {
            NetId = netId;
            MoveType = moveType;
        }
    }
}

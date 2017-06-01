using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class MoveOtherObjectToServer : INetworkRequestToServer
    {
        public int NetId { get; set; }
        public Vector3Serializable Position { get; set; }

        public MoveOtherObjectToServer(int netId, Vector3Serializable position)
        {
            NetId = netId;
            Position = position;
        }
    }
}

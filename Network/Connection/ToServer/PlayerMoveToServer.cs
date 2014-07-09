using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PlayerMoveToServer : INetworkRequestToServer
    {
        public Vector3 NewPosition { get; set; }

        public PlayerMoveToServer(Vector3 newPosition)
        {
            NewPosition = newPosition;
        }
    }
}

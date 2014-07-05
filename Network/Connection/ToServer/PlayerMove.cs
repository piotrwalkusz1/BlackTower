using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PlayerMove : INetworkRequest
    {
        public Vector3 NewPosition { get; set; }

        public PlayerMove(Vector3 newPosition)
        {
            NewPosition = newPosition;
        }
    }
}

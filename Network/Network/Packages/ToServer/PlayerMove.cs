using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class PlayerMove : INetworkPackage
    {
        public Vector3 NewPosition { get; set; }

        public PlayerMove(Vector3 newPosition)
        {
            NewPosition = newPosition;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class AttackToServer : INetworkRequestToServer
    {
        public Vector3Serializable Direction { get; set; }

        public AttackToServer(Vector3 direction)
        {
            Direction = direction;
        }
    }
}

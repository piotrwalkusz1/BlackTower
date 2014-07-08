using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class AttackToServer : INetworkRequest
    {
        public Vector3 Direction { get; set; }

        public AttackToServer(Vector3 direction)
        {
            Direction = direction;
        }
    }
}

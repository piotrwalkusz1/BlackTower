using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class RespawnToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public Vector3Serializable Position { get; set; }

        public RespawnToClient(int idNet, Vector3 position)
        {
            IdNet = idNet;
            Position = position;
        }
    }
}

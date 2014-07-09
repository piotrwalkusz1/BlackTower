using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class RespawnToClient : INetworkRequest
    {
        public int IdNet { get; set; }
        public Vector3 Position { get; set; }

        public RespawnToClient(int idNet, Vector3 position)
        {
            IdNet = idNet;
            Position = position;
        }
    }
}

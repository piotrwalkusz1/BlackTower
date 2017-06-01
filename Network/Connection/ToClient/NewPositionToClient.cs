using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class NewPositionToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public Vector3Serializable Position { get; set; }

        public NewPositionToClient(int idNet, Vector3 newPosition)
        {
            IdNet = idNet;
            Position = newPosition;
        }
    }
}

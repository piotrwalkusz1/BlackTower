using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class MoveToClient : INetworkRequest
    {
        public int IdNet { get; set; }
        public Vector3 NewPosition { get; set; }

        public MoveToClient(int idNet, Vector3 newPosition)
        {
            IdNet = idNet;
            NewPosition = newPosition;
        }
    }
}

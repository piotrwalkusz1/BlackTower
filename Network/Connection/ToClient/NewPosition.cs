using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class NewPosition : INetworkRequest
    {
        public int IdNet { get; set; }
        public Vector3 Position { get; set; }

        public NewPosition(int idNet, Vector3 newPosition)
        {
            IdNet = idNet;
            Position = newPosition;
        }
    }
}

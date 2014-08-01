using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public abstract class CreateToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public bool IsModelVisible { get; set; }
        public Vector3Serializable Position { get; set; }
        public virtual float Rotation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public abstract class Create : INetworkPackage
    {
        public int IdNet { get; set; }
        public Vector3 Position { get; set; }
        public virtual float Rotation { get; set; }
    }
}

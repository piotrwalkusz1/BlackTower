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

        public CreateToClient(int idNet, Vector3Serializable position, float rotation)
            : this(idNet, true, position, rotation)
        {

        }

        public CreateToClient(int idNet, bool isModelVisible, Vector3Serializable position, float rotation)
        {
            IdNet = idNet;
            IsModelVisible = isModelVisible;
            Position = position;
            Rotation = rotation;
        }
    }
}

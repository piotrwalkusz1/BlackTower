using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class NewRotationToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public float Rotation { get; set; }

        public NewRotationToClient(int idNet, float newRotation)
        {
            IdNet = idNet;
            Rotation = newRotation;
        }
    }
}

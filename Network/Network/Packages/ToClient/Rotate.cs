using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class Rotate : INetworkPackage
    {
        public int IdNet { get; set; }
        public float NewRotation { get; set; }

        public Rotate(int idNet, float newRotation)
        {
            IdNet = idNet;
            NewRotation = newRotation;
        }
    }
}

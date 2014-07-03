using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class NewRotation : INetworkPackage
    {
        public int IdNet { get; set; }
        public float NewRotation { get; set; }

        public NewRotation(int idNet, float newRotation)
        {
            IdNet = idNet;
            NewRotation = newRotation;
        }
    }
}

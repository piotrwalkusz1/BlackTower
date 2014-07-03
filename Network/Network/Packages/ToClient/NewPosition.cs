using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class NewPosition : INetworkPackage
    {
        public int IdNet { get; set; }
        public Vector3 NewPosition { get; set; }

        public NewPosition(int idNet, Vector3 newPosition)
        {
            IdNet = idNet;
            NewPosition = newPosition;
        }
    }
}

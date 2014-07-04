﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PlayerRotation : INetworkPackage
    {
        public float NewRotation { get; set; }

        public PlayerRotation(float newRotation)
        {
            NewRotation = newRotation;
        }
    }
}
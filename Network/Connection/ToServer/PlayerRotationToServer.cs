﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PlayerRotationToServer : INetworkRequestToServer
    {
        public float NewRotation { get; set; }

        public PlayerRotationToServer(float newRotation)
        {
            NewRotation = newRotation;
        }
    }
}

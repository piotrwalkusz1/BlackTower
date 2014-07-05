﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class Rotate : INetworkRequest
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

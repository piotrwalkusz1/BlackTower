﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class DeleteObject : INetworkPackage
    {
        public int IdNet { get; set; }

        public DeleteObject(int idNet)
        {
            IdNet = idNet;
        }
    }
}
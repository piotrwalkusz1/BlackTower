﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateMaxExpToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public int MaxExp { get; set; }

        public UpdateMaxExpToClient(int idNet, int maxExp)
        {
            IdNet = idNet;
            MaxExp = maxExp;
        }
    }
}

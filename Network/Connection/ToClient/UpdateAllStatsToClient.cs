﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateAllStatsToClient : INetworkRequest
    {
        public int IdNet { get; set; }
        public IStats Stats { get; set; }

        public UpdateAllStatsToClient(int idNet, IStats stats)
        {
            IdNet = idNet;
            Stats = stats;
        }
    }
}
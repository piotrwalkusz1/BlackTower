using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateAllStatsToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public IStats Stats { get; set; }

        public UpdateAllStatsToClient(int idNet, IStats stats)
        {
            IdNet = idNet;
            Stats = (IStats)Properter.GetProperter(stats);
        }
    }
}

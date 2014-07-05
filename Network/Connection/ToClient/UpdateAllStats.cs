using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateAllStats : INetworkRequest
    {
        public int IdNet { get; set; }
        public IStats Stats { get; set; }

        public UpdateAllStats(int idNet, IStats stats)
        {
            IdNet = idNet;
            Stats = stats;
        }
    }
}

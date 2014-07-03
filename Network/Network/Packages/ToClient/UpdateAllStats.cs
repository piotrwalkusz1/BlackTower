using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class UpdateAllStats : INetworkPackage
    {
        public int IdNet { get; set; }
        public Stats Stats { get; set; }

        public UpdateAllStats(int idNet, Stats stats)
        {
            IdNet = idNet;
            Stats = stats;
        }
    }
}

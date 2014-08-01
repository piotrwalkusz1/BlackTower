using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection
{
    [Serializable]
    public class SpellCasterStatsPackage : StatsPackage, ISpellCasterStats
    {
        public SpellCasterStatsPackage(ISpellCasterStats stats)
        {
            CopyFromStats(stats);
        }

        public int Lvl { get; set; }
    }
}

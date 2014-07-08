using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection
{
    public class ISpellCasterStatsPackage : ISpellCasterStats
    {
        public int Lvl { get; set; }

        public ISpellCasterStatsPackage(ISpellCasterStats stats)
        {
            Lvl = stats.Lvl;
        }
    }
}

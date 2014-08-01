using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection
{
    [Serializable]
    public abstract class StatsPackage : IStats
    {
        public static StatsPackage GetStatsPackage(IStats stats)
        {
            if (stats is IPlayerStats)
                return new PlayerStatsPackage((IPlayerStats)stats);
            else if (stats is ISpellCasterStats)
                return new SpellCasterStatsPackage((ISpellCasterStats)stats);
            else if (stats is IMonsterStats)
                return new MonsterStatsPackage((IMonsterStats)stats);
            else
                throw new NotImplementedException("Nie ma takich statystyk.");
        }

        public void CopyFromStats(IStats stats)
        {
            Copier.CopyAllTargetProperties(stats, this);
        }

        public void CopyToStats(IStats stats)
        {
            Copier.CopyAllSourceProperties(this, stats);
        }
    }
}

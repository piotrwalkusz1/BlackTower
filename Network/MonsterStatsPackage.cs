using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection
{
    [Serializable]
    public class MonsterStatsPackage : StatsPackage, IMonsterStats
    {
        public MonsterStatsPackage(IMonsterStats stats)
        {
            CopyFromStats(stats);
        }

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public int MinDmg { get; set; }

        public int MaxDmg { get; set; }

        public int AttackSpeed { get; set; }

        public float MovementSpeed { get; set; }
    }
}

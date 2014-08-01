using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection
{
    [Serializable]
    public class PlayerStatsPackage : StatsPackage, IPlayerStats
    {
        public PlayerStatsPackage(IPlayerStats stats)
        {
            CopyFromStats(stats);
        }

        public BreedAndGender BreedAndGender { get; private set; }

        public int HP { get; set; }

        public int Lvl { get; set; }

        public int AttackSpeed { get; set; }

        public int MinDmg { get; set; }

        public int MaxDmg { get; set; }

        public int Defense { get; set; }

        public float MovementSpeed { get; set; }

        public int MaxHP { get; set; }

        public float HPRegeneration { get; set; }
    }
}

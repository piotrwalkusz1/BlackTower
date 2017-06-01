using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    [Serializable]
    public class PlayerStatsPackage : StatsPackage, IPlayerStats
    {
        public PlayerStatsPackage()
        {

        }

        public PlayerStatsPackage(IPlayerStats stats)
        {
            CopyFromStats(stats);
        }

        public BreedAndGender BreedAndGender { get; set; }

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public float HPRegeneration { get; set; }

        public int Mana { get; set; }

        public int MaxMana { get; set; }

        public float ManaRegeneration { get; set; }

        public int Lvl { get; set; }

        public int Exp { get; set; }

        public int AttackSpeed { get; set; }

        public int MinDmg { get; set; }

        public int MaxDmg { get; set; }

        public int Defense { get; set; }

        public float MovementSpeed { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Monsters;
using NetworkProject.Connection;

namespace NetworkProject.Monsters
{
    [Serializable]
    public class MonsterStatsInRepository : IMonsterStats
    {
        public MonsterStatsInRepository()
        {
            MovementSpeed = new float[0];
        }

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public int MinDmg { get; set; }

        public int MaxDmg { get; set; }

        public int AttackSpeed { get; set; }

        public float[] MovementSpeed { get; set; }
    }
}

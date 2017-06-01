using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Monsters;
using NetworkProject.Items;
using NetworkProject.Connection;

namespace NetworkProject.Monsters
{
    [Serializable]
	public class MonsterFullData : MonsterData
    {
        public MonsterStatsPackage Stats { get; set; }

        public List<ItemDrop> Drop { get; set; }

        public int MinDmg
        {
            get { return Stats.MinDmg; }
            set { Stats.MinDmg = value; }
        }

        public int MaxDmg
        {
            get { return Stats.MaxDmg; }
            set { Stats.MaxDmg = value; }
        }

        public int AttackSpeed
        {
            get { return Stats.AttackSpeed; }
            set { Stats.AttackSpeed = value; }
        }

        public float[] MovementSpeed
        {
            get { return Stats.MovementSpeed; }
            set { Stats.MovementSpeed = value; }
        }

        public MonsterFullData(int idMonster, IMonsterStats stats)
            : base(idMonster)
        {
            Stats = new MonsterStatsPackage(stats);

            Drop = new List<ItemDrop>();
        }
    } 
}

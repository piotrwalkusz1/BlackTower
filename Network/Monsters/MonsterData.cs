using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Monsters
{
    [Serializable]
    public class MonsterData
    {
        public int IdMonster { get; set; }

        public MonsterData(int idMonster)
        {
            IdMonster = idMonster;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public interface IMonsterStats : IStats
    {
        int HP { get; set; }
        int MaxHP { get; set; }
        int MinDmg { get; set; }
        int MaxDmg { get; set; }
        int AttackSpeed { get; set; }
        float[] MovementSpeed { get; set; }
    }
}

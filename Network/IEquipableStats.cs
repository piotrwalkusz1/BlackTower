using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public interface IEquipableStats : IStats
    {
        int Lvl { get;}
        float AttackSpeed { get; set; }
        int MinDmg { get; set; }
        int MaxDmg { get; set; }
        int Defense { get; set; }
        float MovementSpeed { get; set; }
        int MaxHP { get; set; }
        float HPRegeneration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public interface IPlayerStats : IStats
    {
        BreedAndGender BreedAndGender { get; set; }
        int HP { get; set; }
        int MaxHP { get; set; }
        int Mana { get; set; }
        int MaxMana { get; set; }
        float ManaRegeneration { get; set; }
        int Exp { get; set; }
        int Lvl { get; set; }
        int AttackSpeed { get; set; }
        int MinDmg { get; set; }
        int MaxDmg { get; set; }
        int Defense { get; set; }
        float MovementSpeed { get; set; }
        float HPRegeneration { get; set; }
    }
}

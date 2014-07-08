using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

namespace NetworkProject.Connection
{
    public class PlayerStatsPackage : IPlayerStats
    {
        public BreedAndGender BreedAndGender { get; set; }
        public int HP { get; set; }
        public float AttackSpeed { get; set; }
        public int Defense { get; set; }
        public float HPRegeneration { get; set; }
        public int Lvl { get; set; }
        public int MaxDmg { get; set; }
        public int MaxHP { get; set; }
        public int MinDmg { get; set; }
        public float MovementSpeed { get; set; }

        public PlayerStatsPackage(IPlayerStats stats)
        {
            BreedAndGender = stats.BreedAndGender;
            HP = stats.HP;
            AttackSpeed = stats.AttackSpeed;
            Defense = stats.Defense;
            HPRegeneration = stats.HPRegeneration;
            Lvl = stats.Lvl;
            MaxDmg = stats.MaxDmg;
            MaxHP = stats.MaxHP;
            MinDmg = stats.MinDmg;
            MovementSpeed = stats.MovementSpeed;
        }
    } 
}

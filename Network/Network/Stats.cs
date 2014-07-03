using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    [Serializable]
    public abstract class Stats
    {
        public abstract int Lvl { get; set; }
        public abstract int Hp { get; set; }
        public abstract int MaxHP { get; set; }
        public abstract float RegenerationHP { get; set; }
        public abstract float MovementSpeed { get; set; }
        public abstract float AttackSpeed { get; set; }
        public abstract List<int> dmg { get; set; }
        public abstract int Defense { get; set; }
        public abstract Breed Breed { get; set; }
    }
}

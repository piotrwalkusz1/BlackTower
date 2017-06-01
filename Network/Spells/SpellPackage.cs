using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Spells
{
    [Serializable]
    public class SpellPackage
    {
        public int IdSpell { get; set; }
        public int LvlSpell { get; set; }
        public DateTime NextUseTime { get; set; }

        public SpellPackage(int idSpell, int lvlSpell, DateTime nextUseTime)
        {
            IdSpell = idSpell;
            LvlSpell = lvlSpell;
            NextUseTime = nextUseTime;
        }
    }
}

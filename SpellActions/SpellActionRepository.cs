using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Spells
{
    public static partial class SpellActionsRepository
    {
        public static List<SpellFunction> _spells = new List<SpellFunction>();

        static SpellActionsRepository()
        {

        }

        public static SpellFunction GetSpellAction(int idSpell)
        {
            return _spells[idSpell];
        }
    }
}

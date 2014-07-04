using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.SpellActions
{
    public static partial class SpellActionsRepository
    {
        public static List<SpellFunction> _spells = new List<SpellFunction>();

        static SpellActionsRepository()
        {

        }

        public static SpellFunction GetSpellAction(SpellActionName spell)
        {
            return _spells[(int)spell];
        }
    }
}

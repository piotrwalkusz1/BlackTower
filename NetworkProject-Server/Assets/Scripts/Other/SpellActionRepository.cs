using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;
using NetworkProject.Buffs;

namespace NetworkProject.Spells
{
    public static partial class SpellActionsRepository
    {
        private static List<SpellFunction> _spells = new List<SpellFunction>();

        static SpellActionsRepository()
        {
            _spells.Add(TransformToFire); // 0
        }

        public static SpellFunction GetSpellAction(int idSpell)
        {
            return _spells[idSpell];
        }

        public static void TransformToFire(ISpellCaster caster, params ISpellCastOption[] options) // 0
        {
            DateTime endTimeBuff = DateTime.UtcNow.AddSeconds(7);

            caster.Buffs.AddBuff(new BuffServer(0, endTimeBuff));
        }
    }
}

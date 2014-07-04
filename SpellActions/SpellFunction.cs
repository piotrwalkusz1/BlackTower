using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.SpellActions
{
    public delegate void SpellFunction(ISpellCaster spellCaster, params ISpellCastOption[] options);
}

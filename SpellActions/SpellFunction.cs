using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Spells
{
    public delegate void SpellFunction(ISpellCasterStats spellCaster, params ISpellCastOption[] options);
}

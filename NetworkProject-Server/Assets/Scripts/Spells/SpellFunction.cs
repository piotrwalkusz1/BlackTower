using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

public delegate void SpellFunction(SpellCaster spellCaster, int lvlSpell, params ISpellCastOption[] options);

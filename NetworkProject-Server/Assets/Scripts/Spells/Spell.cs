using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Spells;

public class Spell
{
    public int IdSpell { get; set; }
    public int LvlSpell { get; set; }
    public SpellData SpellData
    {
        get { return SpellRepository.GetSpell(IdSpell); }
    }
    public DateTime NextUseTime { get; set; }

    public Spell(int idSpell, int lvlSpell, DateTime nextUseTime)
    {
        IdSpell = idSpell;
        LvlSpell = lvlSpell;
        NextUseTime = nextUseTime;
    }

    public Spell(int idSpell, int lvlSpell)
        : this(idSpell, lvlSpell, DateTime.UtcNow)
    {
    }

    public bool CanUseSpell(PlayerStats stats)
    {
        return SpellData.AreRequirementsAndManaSatisfy(stats) && DateTime.UtcNow > NextUseTime;
    }

    public bool TryUseSpell(SpellCaster caster, params ISpellCastOption[] options)
    {
        if (CanUseSpell(caster.Stats))
        {
            SpellData spellData = SpellData;

            var spellAction = spellData.GetSpellAction();

            spellAction(caster, LvlSpell, options);

            NextUseTime = DateTime.UtcNow.AddSeconds(spellData.Cooldown);

            caster.Mana -= spellData.ManaCost;

            return true;
        }
        else
        {
            return false;
        }
    }
}

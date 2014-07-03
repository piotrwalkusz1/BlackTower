using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

public class SpellCaster : MonoBehaviour, ISpellCaster
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }
    public int Mana { get; private set; }

    private List<Spell> _spells = new List<Spell>();

    public void AddSpell(Spell spell)
    {
        _spells.Add(spell);
    }

    public void Set(ListPackage<SpellPackage> spells)
    {
        Spell spell;
        SpellData spellData;

        for (int i = 0; i < spells.Count; i++)
        {
            spellData = SpellRepository.GetSpellById(spells[i]._idSpell);
            spell = new Spell(spellData, spells[i]._nextUseTime);

            AddSpell(spell);
        }
    }

    public Spell GetSpellById(int idSpell)
    {
        return _spells.Find(x => x.IdSpell == idSpell);
    }

    public Spell[] GetSpells()
    {
        return _spells.ToArray();
    }

    public void CastSpell(Spell spell)
    {
        spell.UseSpell(this);
    }

    public void CastSpell(Spell spell, params ISpellCastOption[] options)
    {
        spell.UseSpell(this, options);
    }

    public void CastSpell(Spell spell, out string reason)
    {
        spell.UseSpell(this, out reason);
    }

    public void CastSpell(Spell spell, out string reason, params ISpellCastOption[] options)
    {
        spell.UseSpell(this, out reason, options);
    }
}

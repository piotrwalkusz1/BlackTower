using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection.ToServer;
using NetworkProject.Buffs;

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

    public IBuffable Buffs
    {
        get { throw new System.NotImplementedException(); }
    }

    public Vector3 Position
    {
        get { return transform.position; }
    }

    public float Rotation
    {
        get { return transform.eulerAngles.y; }
    }

    public ISpellCasterStats Stats
    {
        get { return GetComponent<PlayerStats>(); }
    }

    public bool TryCastSpellFromSpellBook(int idSpell, params ISpellCastOption[] options)
    {
        Spell spell = _spells.FirstOrDefault(x => x.IdSpell == idSpell);

        if(spell == null || !spell.CanUseSpell(Stats))
        {
            return false;
        }
        else
        {
            spell.UseSpell(this, options);

            return true;
        }
    }

    private List<Spell> _spells = new List<Spell>();

    public void AddSpell(Spell spell)
    {
        AddSpell((SpellClient)spell);
    }

    public void AddSpell(SpellClient spell)
    {
        _spells.Add(spell);
    }

    public void SetSpells(SpellClient[] spells)
    {
        _spells.Clear();

        _spells.AddRange(spells);
    }

    public Spell GetSpellById(int idSpell)
    {
        return _spells.Find(x => x.IdSpell == idSpell);
    }

    public Spell[] GetSpells()
    {
        return _spells.ToArray();
    }
}

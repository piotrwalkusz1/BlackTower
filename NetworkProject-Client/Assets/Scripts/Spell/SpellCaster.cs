using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection.ToServer;
using NetworkProject.Buffs;

public class SpellCaster : ManaInfo
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }

    public Vector3 Position
    {
        get { return transform.position; }
    }

    public float Rotation
    {
        get { return transform.eulerAngles.y; }
    }

    public PlayerStats Stats
    {
        get { return GetComponent<PlayerStats>(); }
    }

    public List<Spell> Spells { get; protected set; }

    void Awake()
    {
        Spells = new List<Spell>();
    }

    public bool TryCastSpellFromSpellBook(int idSpell, params ISpellCastOption[] options)
    {
        Spell spell = Spells.FirstOrDefault(x => x.IdSpell == idSpell);

        if(spell != null)
        {
            return spell.TryUseSpell(this, options); ;
        }
        else
        {
            return false;
        }
    }

    public void SetSpells(Spell[] spells)
    {
        Spells.Clear();

        Spells.AddRange(spells);
    }

    public Spell GetSpellById(int idSpell)
    {
        return Spells.Find(x => x.IdSpell == idSpell);
    }

    public Spell GetSpellBySlot(int slot)
    {
        if (slot >= Spells.Count)
        {
            return null;
        }

        return Spells[slot];
    }

    public bool IsSpell(int idSpell)
    {
        return Spells.Exists(x => x.IdSpell == idSpell);
    }
}

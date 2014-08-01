using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
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
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }
    public float Rotation
    {
        get
        {
            return transform.eulerAngles.y;
        }
    }
    public ISpellCasterStats Stats
    {
        get
        {
            return (ISpellCasterStats)GetComponent(typeof(ISpellCasterStats));
        }
    }
    public IBuffable Buffs
    {
        get { return (IBuffable)GetComponent(typeof(IBuffable)); }
    }

    private List<Spell> _spells;

    public void AddSpell(Spell spell)
    {
        _spells.Add(spell);
    }

    public void SetSpells(Spell[] spells)
    {
        _spells = new List<Spell>(spells);
    }

    public void SetSpells(List<Spell> spells)
    {
        _spells = spells;
    }

    public Spell[] GetSpells()
    {
        return _spells.ToArray();
    }

    public Spell GetSpellById(int idSpell)
    {
        return _spells.Find(x => x.IdSpell == idSpell);
    }

    #region CastSpell

    public bool TryCastSpellFromSpellBook(int idSpell, params ISpellCastOption[] options)
    {
        Spell spell = _spells.Find(x => x.IdSpell == idSpell);

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

    #endregion

    public void SendUpdateSpells()
    {
        int idNet = GetComponent<NetObject>().IdNet;
        var request = new UpdateAllSpellsToClient(idNet, _spells);
    }
}


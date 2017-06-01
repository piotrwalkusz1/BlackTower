using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Buffs;

public class SpellCaster : MonoBehaviour
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }
    public int Mana
    {
        get { return (int)_mana; }
        set { _mana = value; }
    }
    public int MaxMana { get; set; }
    public float ManaRegeneration;
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
    public PlayerStats Stats
    {
        get
        {
            return GetComponent<PlayerStats>();
        }
    }
    public PlayerBuff Buffs
    {
        get { return GetComponent<PlayerBuff>(); }
    }

    private float _mana;

    private List<Spell> _spells;

    void Update()
    {
        int mana = Mana;

        if (_mana < MaxMana)
        {
            _mana += Time.deltaTime * ManaRegeneration;

            if (mana != Mana)
            {
                SendUpdateMana();
            }
        }
    }

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

        if(spell == null)
        {
            MonoBehaviour.print("Nie ma takiego spella w księdze zaklęć.");

            return false;   
        }
        else
        {
            return spell.TryUseSpell(this, options);
        }    
    }

    #endregion

    public void SendUpdateSpells()
    {
        var netPlayer = GetComponent<NetPlayer>();
        var request = new UpdateAllSpellsToClient(netPlayer.IdNet, PackageConverter.SpellToPackage(_spells.ToArray()));

        Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
    }

    public void SendUpdateMana()
    {
        var netPlayer = GetComponent<NetPlayer>();

        var request = new UpdateManaToClient(netPlayer.IdNet, Mana);

        Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
    }

    public bool IsSpell(int idSpell)
    {
        return _spells.Exists(x => x.IdSpell == idSpell);
    }
}


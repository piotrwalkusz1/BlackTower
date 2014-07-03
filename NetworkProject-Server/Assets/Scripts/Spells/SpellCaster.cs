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

    public bool CastSpell(int idSpell)
    {
        string reason = "";
        return CastSpell(idSpell, out reason, new ISpellCastOption[0]);
    }

    public bool CastSpell(int idSpell, params ISpellCastOption[] options)
    {
        string reason = "";
        return CastSpell(idSpell, out reason, options);
    }

    public bool CastSpell(int idSpell, out string reason)
    {
        return CastSpell(idSpell, out reason, new ISpellCastOption[0]);
    }

    public bool CastSpell(int idSpell, out string reason, params ISpellCastOption[] options)
    {
        Spell spell = GetSpellById(idSpell);

        if (spell == null)
        {
            reason = "Caster hasn't this skill.";
            return false;
        }

        return CastSpell(spell, out reason, options);
    }

    public bool CastSpell(Spell spell)
    {
        return spell.UseSpell(this);
    }

    public bool CastSpell(Spell spell, params ISpellCastOption[] options)
    {
        return spell.UseSpell(this, options);
    }

    public bool CastSpell(Spell spell, out string reason)
    {
        return spell.UseSpell(this, out reason);
    }

    public bool CastSpell(Spell spell, out string reason, params ISpellCastOption[] options)
    {
        return spell.UseSpell(this, out reason, options);
    }

    #endregion

    public void SendUpdateSpells()
    {
        var package = GetSpellsPackage();
        Server.SendMessageUpdateYourSpells(package, GetComponent<NetPlayer>().Address);
    }

    public ListPackage<SpellPackage> GetSpellsPackage()
    {
        var spells = new ListPackage<SpellPackage>();

        foreach (Spell spell in _spells)
        {
            spells.Add(new SpellPackage(spell.IdSpell, spell.WhenCanUseSkill()));
        }

        return spells;
    }
}


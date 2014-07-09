using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection.ToServer;

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

    public void SetSpells(Spell[] spells)
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

    public void CastSpellFromSpellBook(Spell spell)
    {
        var request = new UseSpellToServer(spell.IdSpell);

        Client.SendRequestAsMessage(request);
    }
}

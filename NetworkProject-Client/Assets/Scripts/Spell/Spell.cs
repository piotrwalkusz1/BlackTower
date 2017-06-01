using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection.ToServer;

public class Spell
{
    public int IdSpell { get; set; }

    public int LvlSpell { get; set; }

    private DateTime _nextUseTime;

    public SpellData SpellData
    {
        get { return SpellRepository.GetSpell(IdSpell); }
    }

    public Spell(int idSpell, int lvlSpell, DateTime nextUseTime)
    {
        IdSpell = idSpell;
        LvlSpell = lvlSpell;
        _nextUseTime = nextUseTime;
    }

    public bool CanUseSpell(SpellData spellData, PlayerStats stats)
    {
        return spellData.AreRequirementsSatisfy(stats) && DateTime.UtcNow > _nextUseTime;
    }

    public bool TryUseSpell(SpellCaster caster, params ISpellCastOption[] options)
    {
        SpellData spellData = SpellData;

        List<ISpellCastOption> optionsToSend = new List<ISpellCastOption>();

        try
        {
            optionsToSend.AddRange(GiveRequiredInfo(caster, spellData));
        }
        catch
        {
            return false;
        }

        optionsToSend.AddRange(options);      

        if(CanUseSpell(spellData, caster.GetComponent<OwnPlayerStats>()))
        {
            var request = new UseSpellToServer(IdSpell, optionsToSend.ToArray());

            Client.SendRequestAsMessage(request);

            _nextUseTime = DateTime.UtcNow.AddSeconds(SpellData.Cooldown);

            return true;
        }
        else
        {
            return false;
        }
    }

    public ISpellCastOption[] GiveRequiredInfo(SpellCaster spellCaster, SpellData spellData)
    {
        List<ISpellCastOption> options = new List<ISpellCastOption>();

        if (spellData.RequiredInfo._targetPosition)
        {
            Vector3 position = spellCaster.GetComponent<PlayerCamera>().GetLookPointOrDirection();

            options.Add(new SpellCastOptionTargetPosition(position));
        }

        if (spellData.RequiredInfo._targetObject)
        {
            GameObject obj = spellCaster.GetComponent<PlayerCamera>().GetLookObject();

            options.Add(new SpellCastOptionTargetObject(obj.GetComponent<NetObject>().IdNet));
        }
        return options.ToArray();
    }
}

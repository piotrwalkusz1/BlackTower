using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Standard;
using NetworkProject;

public class SpellData
{
    public int IdSpell { get; private set; }
    public string Name
    {
        get
        {
            return Languages.GetSpellName(IdSpell);
        }
    }

    private List<SpellRequirement> _requirements = new List<SpellRequirement>();
    private List<Func<ISpellCaster, string>> _infoToDescription = new List<Func<ISpellCaster,string>>();

    public SpellData(int idSpell)
    {
        IdSpell = idSpell;
    }

    public void AddRequirement(SpellRequirement requirement)
    {
        _requirements.Add(requirement);
    }

    public SpellRequirement[] GetRequirements()
    {
        return _requirements.ToArray();
    }

    public void AddInfoToDescription(Func<ISpellCaster, string> info)
    {
        _infoToDescription.Add(info);
    }

    public string GetDescription(ISpellCaster spellCaster)
    {
        string description = Languages.GetSpellDescription(IdSpell);
        string[] splitDescription = description.Split('$');

        description = FillGapsInDescription(splitDescription, spellCaster);

        return description;
    }

    private string FillGapsInDescription(string[] splitDescription, ISpellCaster spellCaster)
    {
        string description = "";

        for (int i = 0; i < splitDescription.Length - 1; i++)
        {
            description += splitDescription[i] + _infoToDescription[i](spellCaster);
        }

        description += splitDescription[splitDescription.Length - 1];

        return description;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Standard;
using NetworkProject;
using NetworkProject.Requirements;
using NetworkProject.Spells;

public class DescriptedSpellData : SpellData
{
    public string Name
    {
        get
        {
            return Languages.GetSpellName(IdSpell);
        }
    }

    private List<Func<ISpellCaster, string>> _infoToDescription = new List<Func<ISpellCaster,string>>();

    public DescriptedSpellData(int idSpell)
        : base(idSpell)
    {
 
    }

    public DescriptedSpellData(int idSpell, ISpellCasterRequirement[] requirements) : base(idSpell, requirements)
    {

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

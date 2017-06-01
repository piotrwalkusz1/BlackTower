using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Standard;
using NetworkProject;
using NetworkProject.Requirements;
using NetworkProject.Spells;

[Serializable]
public class SpellData
{
    public int IdSpell { get; set; }
    public int IdImage { get; set; }
    public float Cooldown { get ;set; }
    public int ManaCost { get; set; }
    public List<IBenefit> Benefits { get; protected set; }
    public List<IRequirement> Requirements { get; protected set; }
    public SpellRequiredInfo RequiredInfo { get; set; }
    public string Name
    {
        get
        {
            return Languages.GetSpellName(IdSpell);
        }
    }
    private string Description
    {
        get
        {
            return Languages.GetSpellDescription(IdSpell);
        }
    }

    public SpellData()
    {
        Benefits = new List<IBenefit>();
        Requirements = new List<IRequirement>();
        RequiredInfo = new SpellRequiredInfo();
    }

    public SpellData(int idSpell, int idImage, int manaCost, float cooldown, IBenefit[] benefits, IRequirement[] requirements,
         SpellRequiredInfo requiredInfo)
    {
        IdSpell = idSpell;
        IdImage = idImage;
        ManaCost = manaCost;
        Cooldown = cooldown;
        Benefits = new List<IBenefit>(benefits);
        Requirements = new List<IRequirement>(requirements);
        RequiredInfo = requiredInfo;
    }

    public string GetDescription(SpellCaster spellCaster)
    {
        return TextUtility.ReplaceVariablesAndMathExpresionsByNumbers(Description, spellCaster);
    }

    public bool AreRequirementsSatisfy(PlayerStats stats)
    {
        foreach (IRequirement requirement in Requirements)
        {
            if (!requirement.IsRequirementSatisfy(stats))
            {
                return false;
            }
        }

        return true;
    }
}

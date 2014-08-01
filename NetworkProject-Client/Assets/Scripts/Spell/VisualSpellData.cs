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
public class VisualSpellData : SpellData
{
    public int IdImage { get; set; }

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

    public VisualSpellData(int idSpell)
        : base(idSpell)
    {
 
    }

    public VisualSpellData(int idSpell, ISpellCasterRequirement[] requirements) : base(idSpell, requirements)
    {

    }

    public string GetDescription(ISpellCaster spellCaster)
    {
        return TextUtility.ReplaceVariablesAndMathExpresionsByNumbers(Description, spellCaster);
    }
}

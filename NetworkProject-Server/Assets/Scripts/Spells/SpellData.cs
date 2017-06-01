using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Requirements;
using NetworkProject.Spells;

public class SpellData
{
    public int IdSpell { get; private set; }
    public int ManaCost { get; set; }
    public List<IRequirement> Requirements { get; private set; }
    public float Cooldown { get; set; }
    private SpellFunction _spellAction;

    public SpellData(int idSpell, int manaCost, IRequirement[] requirements, float cooldown, SpellFunction spellAction)
    {
        IdSpell = idSpell;
        ManaCost = manaCost;
        Requirements = new List<IRequirement>(requirements);
        Cooldown = cooldown;
        _spellAction = spellAction;
    }

    public SpellFunction GetSpellAction()
    {
        return _spellAction;
    }

    public void SetSpellAction(SpellFunction action)
    {
        _spellAction = action;
    }

    public bool AreRequirementsAndManaSatisfy(PlayerStats stats)
    {
        if (stats.Mana < ManaCost)
        {
            return false;
        }

        foreach (var requirement in Requirements)
        {
            if (!requirement.IsRequirementSatisfy(stats))
            {
                return false;
            }
        }

        return true;
    }
} 

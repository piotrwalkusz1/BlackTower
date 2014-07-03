using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

public class Spell
{
    public int IdSpell
    {
        get
        {
            return SpellData.IdSpell;
        }
    }
    public SpellData SpellData { get; private set; }
    public float Cooldown
    {
        get
        {
            foreach (SpellRequirement req in SpellData.GetRequirements())
            {
                if (req.RequirementType == SpellRequirementType.TimeCooldown)
                {
                    return (float)req.Value;
                }
            }
            return 0f;
        }
    }

    private DateTime _nextUseSkillTime;

    public Spell(SpellData spell) : this(spell, DateTime.UtcNow)
    {
    }

    public Spell(SpellData spell, DateTime nextUseTime)
    {
        SpellData = spell;
        _nextUseSkillTime = nextUseTime;
    }

    public bool UseSpell(ISpellCaster caster, params ISpellCastOption[] options)
    {
        string reason;
        return UseSpell(caster, out reason, options);
    }

    public bool UseSpell(ISpellCaster caster, out string reason, params ISpellCastOption[] options)
    {
        if (CanUseSpell(caster, out reason))
        {
            _nextUseSkillTime = DateTime.UtcNow.AddSeconds(Cooldown);
            
            var package = new SpellCastPackage();
            package._idSpell = IdSpell;

            Client.SendMessageUseSpell(package);

            return true;
        }
        else
        {
            MonoBehaviour.print(reason);
            return false;
        }
    }

    public bool CanUseSpell(ISpellCaster caster)
    {
        string reason;
        return CanUseSpell(caster, out reason);
    }

    public bool CanUseSpell(ISpellCaster caster, out string reason)
    {
        reason = "";
        foreach (SpellRequirement requirement in SpellData.GetRequirements())
        {
            if (!CheckRequirement(caster, requirement, out reason))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckRequirement(ISpellCaster caster, SpellRequirement requirement, out string reason)
    {
        switch (requirement.RequirementType)
        {
            case SpellRequirementType.Lvl:
                return IsRequiredLvl(caster, (int)requirement.Value, out reason);
            case SpellRequirementType.Mana:
                return IsRequiredMana(caster, (int)requirement.Value, out reason);
            case SpellRequirementType.TimeCooldown:
                return IsSkillReady(caster, (float)requirement.Value, out reason);
            default:
                throw new NotImplementedException("Implementation of this requirement's type doesn't exist.");
        }
    }

    private bool IsRequiredLvl(ISpellCaster caster, int lvl, out string reason)
    {
        reason = "";
        if (caster.Lvl >= lvl)
        {
            return true;
        }
        else
        {
            reason = "Required " + lvl.ToString() + " lvl.";
            return false;
        }
    }

    private bool IsRequiredMana(ISpellCaster caster, int mana, out string reason)
    {
        reason = "";
        if (caster.Mana >= mana)
        {
            return true;
        }
        else
        {
            reason = "Required " + mana.ToString() + " mana point";
            return false;
        }
    }

    private bool IsSkillReady(ISpellCaster caster, float cooldown, out string reason)
    {
        reason = "";
        if (DateTime.UtcNow > _nextUseSkillTime)
        {
            return true;
        }
        else
        {
            reason = "Skill hasn't been ready yet.";
            return false;
        }
    }
}

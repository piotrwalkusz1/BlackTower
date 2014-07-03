using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

[System.CLSCompliant(false)]
public delegate void SpellFunction(ISpellCaster spellCaster, params ISpellCastOption[] options);

[System.CLSCompliant(false)]
public class SpellData
{
    public int IdSpell { get; private set; }

    private SpellFunction _spellAction;
    private List<SpellRequirement> _requirements;

    #region Constructors

    public SpellData(int idSpell)
        : this(idSpell, new List<SpellRequirement>())
    {

    }

    public SpellData(int idSpell, params SpellRequirement[] requirements)
        : this(idSpell, new List<SpellRequirement>(requirements))
    {
    }

    public SpellData(int idSpell, List<SpellRequirement> requirements)
        : this(idSpell, null, new List<SpellRequirement>(requirements))
    {
    }

    public SpellData(int idSpell, SpellFunction spellAction, params SpellRequirement[] requirements)
        : this(idSpell, spellAction, new List<SpellRequirement>(requirements))
    {
    }

    public SpellData(int idSpell, SpellFunction spellAction, List<SpellRequirement> requirements)
    {
        IdSpell = idSpell;
        _spellAction = spellAction;
        _requirements = requirements;
    }

    #endregion

    public void CastSpell(ISpellCaster caster, params ISpellCastOption[] options)
    {
        _spellAction(caster, options);
    }

    public void AddRequirement(SpellRequirement requirement)
    {
        _requirements.Add(requirement);
    }

    public void SetSpellAction(SpellFunction action)
    {
        _spellAction = action;
    }

    public SpellRequirement[] GetRequirements()
    {
        return _requirements.ToArray();
    }
}

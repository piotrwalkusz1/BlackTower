using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Requirements;

namespace NetworkProject.Spells
{
    public class SpellActionData : SpellData
    {
        private SpellFunction _spellAction;

        #region Constructors

        public SpellActionData(SpellData spell, SpellFunction spellAction)
            : this(spell.IdSpell, spell.GetRequirements(), spell.Cooldown, spellAction)
        {

        }

        public SpellActionData(int idSpell, SpellFunction spellAction) : base(idSpell)
        {
            _spellAction = spellAction;
        }

        public SpellActionData(int idSpell, ISpellCasterRequirement[] requirements, SpellFunction spellAction) : base(idSpell, requirements)
        {
            _spellAction = spellAction;
        }

        public SpellActionData(int idSpell, ISpellCasterRequirement[] requirements, float Cooldown, SpellFunction spellAction)
            : base(idSpell, requirements, Cooldown)
        {
            _spellAction = spellAction;
        }

        #endregion

        public SpellFunction GetSpellAction()
        {
            return _spellAction;
        }

        public void SetSpellAction(SpellFunction action)
        {
            _spellAction = action;
        }
    } 
}

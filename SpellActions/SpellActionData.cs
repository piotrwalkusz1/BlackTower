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

        public SpellActionData(int idSpell, SpellFunction spellAction) : base(idSpell)
        {
            _spellAction = spellAction;
        }

        public SpellActionData(int idSpell, ISpellCastRequirement[] requirements, SpellFunction spellAction) : base(idSpell, requirements)
        {
            _spellAction = spellAction;
        }

        #endregion

        public void CastSpell(ISpellCasterStats caster, params ISpellCastOption[] options)
        {
                _spellAction(caster, options);        
        }

        public void SetSpellAction(SpellFunction action)
        {
            _spellAction = action;
        }
    } 
}

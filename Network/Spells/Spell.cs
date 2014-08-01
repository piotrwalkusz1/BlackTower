﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Spells
{
    [Serializable]
    public class Spell
    {
        public int IdSpell { get; set; }
        public SpellData SpellData
        {
            get
            {
                return SpellRepository.GetSpell(IdSpell); ;
            }
        }
        public float Cooldown
        {
            get
            {
                return SpellData.Cooldown;
            }
        }

        public Spell(int idSpell)
        {
            IdSpell = idSpell;
        }

        public Spell(int idSpell, DateTime nextUseTime)
        {
            IdSpell = idSpell;
            _nextUseTime = nextUseTime;
        }

        public Spell(SpellData spellData)
        {
            IdSpell = spellData.IdSpell;
        }

        protected DateTime _nextUseTime = DateTime.UtcNow;

        public DateTime GetNextUseTime()
        {
            return _nextUseTime;
        }

        public bool CanUseSpell(ISpellCasterStats stats)
        {
            return DateTime.UtcNow > _nextUseTime && SpellData.CanCastSpell(stats);
        }

        public virtual void UseSpell(ISpellCaster caster, params ISpellCastOption[] options)
        {
            _nextUseTime = DateTime.UtcNow.AddSeconds(Cooldown);

            var spellActionData = (SpellActionData)SpellData;

            var spellAction = spellActionData.GetSpellAction();

            spellAction(caster, options);
        }
    }
}

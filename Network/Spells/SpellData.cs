using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;

namespace NetworkProject.Spells
{
    public class SpellData
    {
        public int IdSpell { get; private set; }

        public float Cooldown { get; set; }

        private List<ISpellCastRequirement> _requirements = new List<ISpellCastRequirement>();

        public SpellData(int idSpell)
        {
            IdSpell = idSpell;
        }

        public SpellData(int idSpell, ISpellCastRequirement[] requirements)
            : this(idSpell)
        {
            _requirements.AddRange(requirements);
        }

        public void AddRequirement(ISpellCastRequirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(ISpellCastRequirement[] requirements)
        {
            _requirements.AddRange(requirements);
        }

        public bool CanCastSpell(ISpellCasterStats stats)
        {
            foreach (IRequirement requirement in _requirements)
            {
                if (!requirement.IsRequirementSatisfy(stats))
                {
                    return false;
                }
            }

            return true;
        }

        public ISpellCastRequirement[] GetRequirements()
        {
            return _requirements.ToArray();
        }
    }
}

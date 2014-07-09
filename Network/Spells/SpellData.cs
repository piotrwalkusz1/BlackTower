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

        private List<ISpellCasterRequirement> _requirements = new List<ISpellCasterRequirement>();

        public SpellData(int idSpell)
        {
            IdSpell = idSpell;
        }

        public SpellData(int idSpell, ISpellCasterRequirement[] requirements)
            : this(idSpell)
        {
            _requirements.AddRange(requirements);
        }

        public void AddRequirement(ISpellCasterRequirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(ISpellCasterRequirement[] requirements)
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

        public ISpellCasterRequirement[] GetRequirements()
        {
            return _requirements.ToArray();
        }
    }
}

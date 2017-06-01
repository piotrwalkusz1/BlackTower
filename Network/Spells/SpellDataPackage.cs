using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;

namespace NetworkProject.Spells
{
    [Serializable]
    public class SpellDataPackage
    {
        public int IdSpell { get; set; }
        public int ManaCost { get; set; }
        public float Cooldown { get; set; }
        public SpellRequiredInfo RequiredInfo { get; set; }

        private List<IRequirementPackage> _requirements = new List<IRequirementPackage>();

        public SpellDataPackage(int idSpell, int manaCost, IRequirementPackage[] requirements, float cooldown,
            SpellRequiredInfo requiredInfo)
        {
            IdSpell = idSpell;
            ManaCost = manaCost;
            _requirements = new List<IRequirementPackage>(requirements);
            Cooldown = cooldown;
            RequiredInfo = requiredInfo;
        }

        public void AddRequirement(IRequirementPackage requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(IRequirementPackage[] requirements)
        {
            _requirements.AddRange(requirements);
        }

        public IRequirementPackage[] GetRequirements()
        {
            return _requirements.ToArray();
        }
    }
}

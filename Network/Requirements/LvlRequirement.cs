using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    [Serializable]
    public class LvlRequirement : IEquipRequirement, ISpellCasterRequirement
    {
        private int _requiredLvl;

        public LvlRequirement()
        {

        }

        public LvlRequirement(string requiredLvl)
        {
            _requiredLvl = int.Parse(requiredLvl);
        }

        public LvlRequirement(int requiredLvl)
        {
            _requiredLvl = requiredLvl;
        }

        public void Set(string value)
        {
            _requiredLvl = int.Parse(value);
        }

        public string GetAsString()
        {
            return _requiredLvl.ToString();
        }

        public bool IsRequirementSatisfy(IStats stats)
        {
            if (stats is IEquipableStats)
            {
                return IsRequirementSatisfy((IEquipableStats)stats);
            }

            if (stats is ISpellCasterStats)
            {
                return IsRequirementSatisfy((ISpellCasterStats)stats);
            }

            return false;
        }

        public bool IsRequirementSatisfy(IEquipableStats stats)
        {
            return stats.Lvl >= _requiredLvl;
        }

        public bool IsRequirementSatisfy(ISpellCasterStats stats)
        {
            return stats.Lvl >= _requiredLvl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    public class RequirementLvl : Requirement
    {
        private int _requiredLvl;

        public RequirementLvl(string value)
        {
            _requiredLvl = int.Parse(value);
        }

        public override bool IsRequirementSatisfy(IStats stats)
        {
            return stats.Lvl >= _requiredLvl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    [Serializable]
    public class LvlRequirementPackage : IRequirementPackage
    {
        public int RequiredLvl { get; set; }

        public LvlRequirementPackage(int requiredLvl)
        {
            RequiredLvl = requiredLvl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    public abstract class Requirement
    {
        public abstract bool IsRequirementSatisfy(Stats stats);
    }
}

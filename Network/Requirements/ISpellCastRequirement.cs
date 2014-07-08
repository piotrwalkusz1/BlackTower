using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    public interface ISpellCastRequirement : IRequirement
    {
        bool IsRequirementSatisfy(ISpellCasterStats stats);
    }
}

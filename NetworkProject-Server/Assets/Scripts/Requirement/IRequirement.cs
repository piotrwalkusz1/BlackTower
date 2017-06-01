using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IRequirement
{
    bool IsRequirementSatisfy(PlayerStats stats);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LvlRequirement : IRequirement
{
    public int Value { get; set; }

    public LvlRequirement(int value)
    {
        Value = value;
    }

    public bool IsRequirementSatisfy(PlayerStats stats)
    {
        return stats.Lvl >= Value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Requirements;

[Serializable]
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

    public void Set(string value)
    {
        Value = int.Parse(value);
    }

    public string GetAsString()
    {
        return Value.ToString();
    }

    public IRequirementPackage ToPackage()
    {
        return new LvlRequirementPackage(Value);
    }
}

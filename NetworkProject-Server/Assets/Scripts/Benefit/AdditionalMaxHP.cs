using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

public class AdditionalMaxHP : IBenefit
{
    public int Value { get; set; }

    public AdditionalMaxHP(int value)
    {
        Value = value;
    }

    public void ApplyToStats(IPlayerStats stats)
    {
        stats.MaxHP += Value;
    }
}

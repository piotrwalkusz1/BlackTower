using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class HelmetData : EquipableItemData
{
    public int _defense;

    public HelmetData(int idItem, IBenefit[] benefits, IRequirement[] requirements, int defense)
        : base(idItem, benefits, requirements)
    {
        _defense = defense;
    }

    public override void ApplyItemStats(PlayerStats stats)
    {
        stats.Defense += _defense;
    }
}

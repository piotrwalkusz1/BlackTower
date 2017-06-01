using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class ArmorData : EquipableItemData
{
    public int _defense;

    public ArmorData(int idItem, IBenefit[] benefits, IRequirement[] requirements, int defense)
        : base(idItem, benefits, requirements)
    {
        _defense = defense;
    }

    public override void ApplyItemStats(PlayerStats stats)
    {
        stats.Defense += _defense;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class ShoesData : EquipableItemData
{
    public float _movementSpeed;

    public ShoesData(int idItem, IBenefit[] benefits, IRequirement[] requirements, float movementSpeed)
        : base(idItem, benefits, requirements)
    {
        _movementSpeed = movementSpeed;
    }

    public override void ApplyItemStats(PlayerStats stats)
    {
        stats.MovementSpeed += _movementSpeed;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class WeaponData : EquipableItemData
{
    public int _minDmg;
    public int _maxDmg;
    public int _attackSpeed;

    public WeaponData(int idItem, IBenefit[] benefits, IRequirement[] requirements, int minDmg, int maxDmg, int attackSpeed)
        : base(idItem, benefits, requirements)
    {
        _minDmg = minDmg;
        _maxDmg = maxDmg;
        _attackSpeed = attackSpeed;
    }

    public override void ApplyItemStats(PlayerStats stats)
    {
        stats.MinDmg += _minDmg;
        stats.MaxDmg += _maxDmg;
        stats.AttackSpeed += _attackSpeed;
    }
}

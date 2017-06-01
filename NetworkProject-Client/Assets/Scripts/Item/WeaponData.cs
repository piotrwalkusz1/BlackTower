using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject;
using NetworkProject.Items;

[Serializable]
public class WeaponData : EquipableItemData
{
    public int _minDmg;
    public int _maxDmg;
    public int _attackSpeed;

    public WeaponData(int idItem)
        : base(idItem)
    {
        
    }

    public override ItemDataPackage ToPackage()
    {
        return new WeaponPackage(IdItem, PackageConverter.BenefitToPackage(GetBenefits()),
            PackageConverter.RequirementToPackage(GetRequirements()).ToArray(), _minDmg, _maxDmg, _attackSpeed);
    }

    public override string GetDescription()
    {
        string result = Languages.GetItemName(IdItem);
        result += "\n\n";
        result += Languages.GetPhrase("damage") + " : " + _minDmg.ToString() + " - " + _maxDmg.ToString() + '\n';
        result += Languages.GetPhrase("attack speed") + " : " + _attackSpeed.ToString();

        string benefits = GetBenefitsDescription();

        if (benefits != "")
        {
            result += '\n';
            result += benefits;
        }

        return result;
    }
}

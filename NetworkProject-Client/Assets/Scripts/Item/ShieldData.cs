using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject;
using NetworkProject.Items;

[Serializable]
public class ShieldData : EquipableItemData
{
    public int _defense;

    public ShieldData(int idItem) : base(idItem)
    {

    }

    public override ItemDataPackage ToPackage()
    {
        return new ShieldPackage(IdItem, PackageConverter.BenefitToPackage(GetBenefits()),
            PackageConverter.RequirementToPackage(GetRequirements()).ToArray(), _defense);
    }

    public override string GetDescription()
    {
        string result = Languages.GetItemName(IdItem);
        result += "\n\n";
        result += Languages.GetPhrase("defense") + " : " + _defense.ToString();

        string benefits = GetBenefitsDescription();

        if (benefits != "")
        {
            result += '\n';
            result += benefits;
        }

        return result;
    }
}

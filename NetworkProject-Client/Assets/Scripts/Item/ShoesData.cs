using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject;
using NetworkProject.Items;

[Serializable]
public class ShoesData : EquipableItemData
{
    public float _movementSpeed;

    public ShoesData(int idItem)
        : base(idItem)
    {

    }

    public override ItemDataPackage ToPackage()
    {
        return new ShoesPackage(IdItem, PackageConverter.BenefitToPackage(GetBenefits()),
            PackageConverter.RequirementToPackage(GetRequirements()).ToArray(), _movementSpeed);
    }

    public override string GetDescription()
    {
        string result = Languages.GetItemName(IdItem);
        result += "\n\n";
        result += Languages.GetPhrase("movement speed") + " : " + _movementSpeed.ToString();

        string benefits = GetBenefitsDescription();

        if (benefits != "")
        {
            result += '\n';
            result += benefits;
        }

        return result;
    }
}

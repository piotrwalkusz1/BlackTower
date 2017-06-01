using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Standard;
using NetworkProject;
using NetworkProject.Items;

[Serializable]
public class AdditionData : EquipableItemData
{
    public AdditionData(int idItem)
        : base(idItem)
    {

    }

    public override ItemDataPackage ToPackage()
    {
        return new AdditionPackage(IdItem, PackageConverter.BenefitToPackage(GetBenefits()),
            PackageConverter.RequirementToPackage(GetRequirements()).ToArray());
    }

    public override string GetDescription()
    {
        return base.GetDescription();
    }
}

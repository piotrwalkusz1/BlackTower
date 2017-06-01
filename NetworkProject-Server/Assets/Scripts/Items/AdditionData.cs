using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NetworkProject;
using NetworkProject.Items;

public class AdditionData : EquipableItemData
{
    public AdditionData(int idItem, IBenefit[] benefits, IRequirement[] requirements)
        : base(idItem, benefits, requirements)
    {

    }

    public override void ApplyItemStats(PlayerStats stats)
    {
      
    }
}

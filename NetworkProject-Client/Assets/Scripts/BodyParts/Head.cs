using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class Head : BodyPart
{
    public Head(Item item)
        : base(item)
    {

    } 

    public override bool CanEquipeItemOnThisBodyPart(EquipableItemData item)
    {
        return item is HelmetData;
    }
}

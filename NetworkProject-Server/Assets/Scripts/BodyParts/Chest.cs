using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public class Chest : BodyPart
{
    public Chest(Item item)
        : base(item)
    {

    } 

    public override bool CanEquipeItemOnThisBodyPart(EquipableItemData item)
    {
        return item is ArmorData;
    }
}

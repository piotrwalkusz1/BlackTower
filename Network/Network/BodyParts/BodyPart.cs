using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    public abstract class BodyPart
    {
        public Item EquipedItem { get; set; }

        public abstract bool CanEquipeItemOnThisBodyPart(ItemInfo item);
    }
}



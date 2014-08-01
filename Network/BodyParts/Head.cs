using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class Head : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(EquipableItemData item)
        {
            return item is HelmetData;
        }
    }
}

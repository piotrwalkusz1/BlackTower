using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    public class LeftHand : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(ItemData item)
        {
            return item is ShieldData;
        }
    }
}

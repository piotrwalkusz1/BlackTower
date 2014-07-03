using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

namespace NetworkProject.BodyParts
{
    public class Feet : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(ItemInfo item)
        {
            return item is ShoesInfo;
        }
    }
}

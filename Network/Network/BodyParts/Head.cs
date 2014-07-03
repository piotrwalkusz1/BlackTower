using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

namespace NetworkProject.BodyParts
{
    public class Head : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(NetworkProject.ItemInfo item)
        {
            return item is HelmetInfo;
        }
    }
}

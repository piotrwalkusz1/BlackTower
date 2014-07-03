﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

namespace NetworkProject.BodyParts
{
    public class Other : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(ItemInfo item)
        {
            return item is AdditionInfo;
        }
    }
}

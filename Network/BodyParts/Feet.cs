﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items.Repository;

namespace NetworkProject.BodyParts
{
    public class Feet : BodyPart
    {
        public override bool CanEquipeItemOnThisBodyPart(Item item)
        {
            return item is Shoes;
        }
    }
}
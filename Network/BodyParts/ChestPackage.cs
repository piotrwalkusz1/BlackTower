using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class ChestPackage : BodyPartPackage
    {
        public ChestPackage()
        {

        }

        public ChestPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

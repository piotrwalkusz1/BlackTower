using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class HeadPackage : BodyPartPackage
    {
        public HeadPackage()
        {

        }

        public HeadPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

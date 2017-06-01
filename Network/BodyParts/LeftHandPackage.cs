using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class LeftHandPackage : BodyPartPackage
    {
        public LeftHandPackage()
        {

        }

        public LeftHandPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

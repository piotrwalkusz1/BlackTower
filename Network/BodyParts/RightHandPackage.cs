using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class RightHandPackage : BodyPartPackage
    {
        public RightHandPackage()
        {

        }

        public RightHandPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

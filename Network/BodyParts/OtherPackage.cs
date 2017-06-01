using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class OtherPackage : BodyPartPackage
    {
        public OtherPackage()
        {

        }

        public OtherPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

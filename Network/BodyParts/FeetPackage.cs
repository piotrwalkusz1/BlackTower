using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public class FeetPackage : BodyPartPackage
    {
        public FeetPackage()
        {

        }

        public FeetPackage(ItemPackage item)
            : base(item)
        {

        }
    }
}

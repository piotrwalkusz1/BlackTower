using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public abstract class BodyPartPackage
    {
        public ItemPackage EquipedItem { get; set; }

        public BodyPartPackage()
        {

        }

        public BodyPartPackage(ItemPackage item)
        {
            EquipedItem = item;
        }
    }
}



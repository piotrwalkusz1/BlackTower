using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class ChangeEquipedItems : INetworkPackage
    {
        public BodyPartType EquipedItem1;
        public BodyPartType EquipedItem2;

        public ChangeEquipedItems(BodyPartType equipedItem1, BodyPartType equipedItem2)
        {
            EquipedItem1 = equipedItem1;
            EquipedItem2 = equipedItem2;
        }
    }
}

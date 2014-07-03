using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using NetworkProject.Items;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class UpdateEquipedItem : INetworkPackage
    {
        public int IdNet { get; set; }
        public BodyPartType Slot { get; set; }
        public Item Item { get; set; }

        public UpdateEquipedItem(int idNet, BodyPartType slot, Item item)
        {
            IdNet = idNet;
            Slot = slot;
            Item = item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using UnityEngine;

namespace NetworkProject.Items
{
    [Serializable]
    public class PlayerEquipedItemsPackage
    {
        public List<BodyPartPackage> BodyParts { get; set; }

        public PlayerEquipedItemsPackage(List<BodyPartPackage> bodyParts)
        {
            BodyParts = bodyParts;
        }
    }
}


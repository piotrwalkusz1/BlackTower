using UnityEngine;
using System;
using System.Collections;

namespace NetworkProject.Items
{
    [Serializable]
    public class ItemPackage
    {
        public int IdItem { get; set; }

        public ItemPackage(int idItem)
        {
            IdItem = idItem;
        }
    }
}


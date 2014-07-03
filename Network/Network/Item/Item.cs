using UnityEngine;
using System;
using System.Collections;

namespace NetworkProject.Items
{
    [Serializable]
    public class Item
    {
        public int IdItem { get; set; }

        public Item(int idItem)
        {
            IdItem = idItem;
        }
    }
}


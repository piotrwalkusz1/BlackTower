using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateItem : Create
    {
        public int IdItem { get; set; }

        public CreateItem(int idNet, Vector3 position, float rotation, int idItem)
        {
            IdNet = idNet;
            IdItem = idItem;
            Position = position;
            Rotation = rotation;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class CreateVisualObject : Create
    {
        public int IdVisualObject { get; set; }

        public CreateVisualObject(int idNet, Vector3 position, float rotation, int idVisualObject)
        {
            IdNet = idNet;
            Position = position;
            Rotation = rotation;
            IdVisualObject = idVisualObject;
        }
    }
}

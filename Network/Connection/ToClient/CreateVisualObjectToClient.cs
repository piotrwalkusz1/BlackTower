using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateVisualObjectToClient : CreateToClient
    {
        public int IdVisualObject { get; set; }

        public CreateVisualObjectToClient(int idNet, Vector3 position, float rotation, int idVisualObject)
            : base(idNet, position, rotation)
        {
            IdVisualObject = idVisualObject;
        }
    }
}

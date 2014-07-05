using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class Attack : INetworkRequest
    {
        public Vector3 RotationAttack { get; set; }

        public Attack(Vector3 rotationAttack)
        {
            RotationAttack = rotationAttack;
        }
    }
}

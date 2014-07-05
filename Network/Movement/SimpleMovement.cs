using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Movement
{
    public class SimpleMovement : MonoBehaviour
    {
        public float Speed { get; set; }

        void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}

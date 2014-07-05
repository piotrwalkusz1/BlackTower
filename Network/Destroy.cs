using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class Destroy : MonoBehaviour
    {
        public float RemainLifeTime { get; set; }

        void Update()
        {
            RemainLifeTime -= Time.deltaTime;

            if (RemainLifeTime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}

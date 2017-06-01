using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 offset = Camera.main.transform.position - transform.position;

        offset.y = 0;

        transform.rotation = Quaternion.LookRotation(offset);
    }
}

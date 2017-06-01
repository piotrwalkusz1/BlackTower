using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float _speed;
    public bool _freezeXZ;

    void Update()
    {
        transform.Rotate(Vector3.up, _speed * Time.deltaTime);

        if (_freezeXZ)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }  
    }
}

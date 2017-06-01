using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float _time;

    void Awake()
    {
        Destroy(gameObject, _time);
    }
}

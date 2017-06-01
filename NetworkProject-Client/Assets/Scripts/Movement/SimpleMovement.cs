using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleMovement : Movement
{
    public float _speed;

    private Vector3 _target;

    void Start()
    {
        _target = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public override void SetNewTargetPosition(Vector3 position)
    {
        _target = position;
    }
}

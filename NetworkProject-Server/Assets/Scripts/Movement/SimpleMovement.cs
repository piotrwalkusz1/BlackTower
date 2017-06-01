using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleMovement : Movement
{
    public float _speed;

    protected Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public override void SetNewPosition(Vector3 newPosition)
    {
        _targetPosition = newPosition;
    }
}

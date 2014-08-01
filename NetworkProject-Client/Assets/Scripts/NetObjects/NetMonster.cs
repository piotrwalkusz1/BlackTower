using UnityEngine;
using System.Collections;

public class NetMonster : NetObject
{
    private Vector3 _lastPosition;
    private bool _wasChangePosition;
    private bool _wasFall;

    protected void Awake()
    {
        _lastPosition = transform.position;
    }

    protected void Update()
    {
        _wasChangePosition = transform.position != _lastPosition;
        _wasFall = transform.position.y < _lastPosition.y;

        _lastPosition = transform.position;
    }

    public bool IsMovement()
    {
        return _wasChangePosition;
    }

    public bool IsFall()
    {
        return _wasFall;
    }
}

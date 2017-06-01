using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PhysicsMonsterMovement : MonsterMovement
{
    private Vector3 _moveTargetPosition;

    protected void Awake()
    {
        _moveTargetPosition = transform.position;
    }

    protected void Update()
    {
        Vector3 movement = Vector3.zero;

        if (!IsStop())
        {
            RotateToTarget();

            Vector3 newVelocity = transform.TransformDirection(Vector3.forward * MoveSpeed); ;
            newVelocity.y = rigidbody.velocity.y;

            rigidbody.velocity = newVelocity;
        }
    }

    public override void SetNewPosition(Vector3 newPosition)
    {
        _moveTargetPosition = newPosition;
    }

    public bool IsStop()
    {
        Vector3 offset = transform.position - _moveTargetPosition;

        offset.y = 0;

        return offset.sqrMagnitude < 0.3f;
    }

    public override void Stop()
    {
        _moveTargetPosition = transform.position;
    }

    protected void RotateToTarget()
    {
        Vector3 newDir = _moveTargetPosition - transform.position;

        newDir.y = 0;

        rigidbody.MoveRotation(Quaternion.LookRotation(newDir));
    }
}

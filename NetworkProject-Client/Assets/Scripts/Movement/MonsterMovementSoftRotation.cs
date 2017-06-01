using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterMovementSoftRotation : MonsterMovement
{
    public float _rotationSpeed;
    protected float _targetRotation;

    void Update()
    {
        float rot = Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation, _rotationSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, rot, 0);
    }

    public override void SetNewRotation(float rotation)
    {
        _targetRotation = rotation;
    }
}

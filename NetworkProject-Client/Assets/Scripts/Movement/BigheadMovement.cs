using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BigheadMovement : MonsterMovement
{
    private Vector3 _targetPosition;

    private SpeedMonsterAnimation _animation;

    void Start()
    {
        _targetPosition = transform.position;

        _animation = GetComponent<SpeedMonsterAnimation>();
    }

    void Update()
    {
        if (IsStop())
        {
            _animation.SetSpeed(0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, CurrentMovementSpeed * Time.deltaTime);

            _animation.SetSpeed(CurrentMovementSpeed);
        }   
    }

    public override void SetNewTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    public override void SetNewRotation(float rotation)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }

    public bool IsStop()
    {
        return transform.position.x == _targetPosition.x && transform.position.z == _targetPosition.z;
    }
}

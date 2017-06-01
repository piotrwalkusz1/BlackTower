using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToServer;

public class NetTakeOverPosition : NetObject
{
    private Vector3 _positionInLastFrame;
    private bool _wasMovementInLastFrame;

    protected void Update()
    {
        if (IsMovement())
        {
            var request = new MoveOtherObjectToServer(IdNet, transform.position);

            Client.SendRequestAsMessage(request);
        }
    }

    protected void LateUpdate()
    {
        CheckChangePositionAndRotation();
    }

    public bool IsMovement()
    {
        return _wasMovementInLastFrame;
    }

    protected void CheckChangePositionAndRotation()
    {
        _wasMovementInLastFrame = transform.position != _positionInLastFrame;

        _positionInLastFrame = transform.position;
    }


}

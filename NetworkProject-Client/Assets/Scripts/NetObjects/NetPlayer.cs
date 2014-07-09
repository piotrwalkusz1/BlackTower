using UnityEngine;
using System.Collections;
using NetworkProject;

public class NetPlayer : NetNamedObject
{
    public BreedAndGender BreedAndGender { get; set; }

    private Vector3 _positionInLastFrame;
    private bool _wasMovementInLastFrame;

    private float _rotationInLastFrame;
    private bool _wasRotationInLastFrame;

    protected void Awake()
    {
        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }

    protected void LateUpdate()
    {
        CheckChangePositionAndRotation();
    }

    public bool IsMovement()
    {
        return _wasMovementInLastFrame;
    }

    public bool IsRotation()
    {
        return _wasRotationInLastFrame;
    }

    public bool IsFall()
    {
        return transform.position.y < _positionInLastFrame.y;
    }

    protected void CheckChangePositionAndRotation()
    {
        _wasMovementInLastFrame = transform.position != _positionInLastFrame;
        _wasRotationInLastFrame = transform.eulerAngles.y != _rotationInLastFrame;

        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }
}

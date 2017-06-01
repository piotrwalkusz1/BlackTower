using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

public class NetPlayer : NetNamedObject
{
    public BreedAndGender BreedAndGender { get; set; }
    public int TransformModel { get; set; }
    public bool IsTransformed
    {
        get { return TransformModel == -1; }
    }

    private Vector3 _positionInLastFrame;
    private bool _wasMovementInLastFrame;

    private float _rotationInLastFrame;
    private bool _wasRotationInLastFrame;

    protected void Awake()
    {
        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
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

    public override void Respawn(RespawnToClient respawnInfo)
    {
        base.Respawn(respawnInfo);

        GetComponent<PlayerGeneratorModel>().ShowModel();
        
        var characterMotor = GetComponent<CharacterMotor>();

        if (characterMotor != null)
        {
            characterMotor.enabled = true;
        }
    }

    protected void CheckChangePositionAndRotation()
    {
        _wasMovementInLastFrame = transform.position != _positionInLastFrame;
        _wasRotationInLastFrame = transform.eulerAngles.y != _rotationInLastFrame;

        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }
}

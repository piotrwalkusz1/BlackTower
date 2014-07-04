using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public abstract class NetObject : MonoBehaviour 
{
    public int IdNet { get; set; }

    protected Func<Vision, bool> _isVisible;

    private Vector3 _positionInLastFrame;
    private bool _wasMovementInLastFrame;

    private float _rotationInLastFrame;
    private bool _wasRotationInLastFrame;

    private bool _isHpMessage;
    private bool _isHpMessageInLastFrame;

    private bool _isMaxHpMessage;
    private bool _isMaxHpMessageInLastFrame;

    protected void Awake()
    {
        VisionFunctionToDefault();

        InitializePositionAndRotation();
    }

    protected void LateUpdate()
    {
        MessageFlagsToDefault();
    }

    public bool IsVisible(Vision vision)
    {
        return _isVisible(vision);
    }

    public void SetMethodIsVisible(Func<Vision, bool> function)
    {
        _isVisible = function;
    }

    public virtual bool IsMustUpdate()
    {
        return (IsChangePosition() || IsChangeRotation() || _isHpMessageInLastFrame || _isMaxHpMessageInLastFrame);
    }

    public int GetMap()
    {
        return Standard.Settings.GetMap(transform.position);
    }

    public abstract void SendMessageAppeared(IConnectionMember address);

    public abstract void SendMessageUpdate(IConnectionMember address);

    public abstract void SendMessageDisappeared(IConnectionMember address);

    public void SendChangeHpMessage()
    {
        _isHpMessage = true;
    }

    public void SendChangeMaxHpMessage()
    {
        _isMaxHpMessage = true;
    }

    protected void MessageFlagsToDefault()
    {
        CheckChangePositionAndRotation();

        _isHpMessageInLastFrame = _isHpMessage;
        _isHpMessage = false;

        _isMaxHpMessageInLastFrame = _isMaxHpMessage;
        _isMaxHpMessage = false;
    }

    protected void VisionFunctionToDefault()
    {
        _isVisible = delegate(Vision vision) { return true; };
    }

    protected void IfFlagSendHpUpdate(IConnectionMember address)
    {
        if (_isHpMessageInLastFrame)
        {
            Server.SendMessageUpdateOtherHp(IdNet, GetComponent<HealthSystem>().HP, address);
        }
    }

    protected void IfFlagSendMaxHpUpdate(IConnectionMember address)
    {
        if (_isMaxHpMessageInLastFrame)
        {
            Server.SendMessageUpdateOtherMaxHp(IdNet, GetComponent<HealthSystem>().MaxHP, address);
        }
    }

    protected void IfChangeSendPositionUpdate(IConnectionMember address)
    {
        if (IsChangePosition())
        {
            Server.SendMessageNewPosition(IdNet, transform.position, address);
        }
    }

    protected void IfChangeSendRotationUpdate(IConnectionMember address)
    {
        if (IsChangeRotation())
        {
            Server.SendMessageNewRotation(IdNet, transform.eulerAngles.y, address);
        }
    }

    protected void CheckChangePositionAndRotation()
    {
        _wasMovementInLastFrame = transform.position != _positionInLastFrame;
        _wasRotationInLastFrame = transform.eulerAngles.y != _rotationInLastFrame;

        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }

    protected bool IsChangePosition()
    {
        return _wasMovementInLastFrame;
    }

    protected bool IsChangeRotation()
    {
        return _wasRotationInLastFrame;
    }

    protected void InitializePositionAndRotation()
    {
        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }
}

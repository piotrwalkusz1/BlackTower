using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public abstract class NetObject : MonoBehaviour 
{
    public int IdNet { get; set; }

    protected Func<Vision, bool> _isVisible;

    protected event Action<IConnectionMember> _sendMessageUpdateEvent;

    private Vector3 _positionInLastFrame;
    private bool _wasMovementInLastFrame;

    private float _rotationInLastFrame;
    private bool _wasRotationInLastFrame;

    protected void Awake()
    {
        SetVisionFunctionToDefault();

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

    public bool IsMustUpdate()
    {
        return (IsChangePosition() || IsChangeRotation() || _sendMessageUpdateEvent != null);
    }

    public int GetMap()
    {
        return Standard.Settings.GetMap(transform.position);
    }

    public abstract void SendMessageAppeared(IConnectionMember address);

    public void SendMessageUpdate(IConnectionMember address)
    {
        IfChangeSendPositionUpdate(address);

        IfChangeSendRotationUpdate(address);

        InvokeSendMessageUpdateEvent(address);
    }

    public abstract void SendMessageDisappeared(IConnectionMember address);

    public void SendChangeHpMessage()
    {
        _sendMessageUpdateEvent += SendChangeHpMessageFuntion;
    }

    public void SendChangeMaxHpMessage()
    {
        _sendMessageUpdateEvent += SendChangeMaxHpMessageFunction;
    }

    protected void MessageFlagsToDefault()
    {
        CheckChangePositionAndRotation();

        _sendMessageUpdateEvent = null;
    }

    protected void SetVisionFunction(Func<Vision, bool> function)
    {
        _isVisible = function;
    }

    protected void IfChangeSendPositionUpdate(IConnectionMember address)
    {
        if (IsChangePosition())
        {
            var package = new Rotate(IdNet, transform.eulerAngles.y);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        }
    }

    protected void IfChangeSendRotationUpdate(IConnectionMember address)
    {
        if (IsChangeRotation())
        {
            var package = new Move(IdNet, transform.position);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
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

    protected void InvokeSendMessageUpdateEvent(IConnectionMember address)
    {
        if (_sendMessageUpdateEvent != null)
        {
            _sendMessageUpdateEvent(address);
        }
    }

    protected void SetVisionFunctionToDefault()
    {
        SetVisionFunction(DefaultVisionFunction);
    }

    private void SendChangeHpMessageFuntion(IConnectionMember address)
    {
        int hp = GetComponent<HealthSystem>().HP;
        var package = new UpdateHP(IdNet, hp);
        var message = new OutgoingMessage(package);

        Server.Send(message, address);
    }

    private void SendChangeMaxHpMessageFunction(IConnectionMember address)
    {
        int maxHp = GetComponent<HealthSystem>().MaxHP;
        var package = new UpdateMaxHP(IdNet, maxHp);
        var message = new OutgoingMessage(package);

        Server.Send(message, address);
    }

    private bool DefaultVisionFunction(Vision vision)
    {
        return true;
    }
}

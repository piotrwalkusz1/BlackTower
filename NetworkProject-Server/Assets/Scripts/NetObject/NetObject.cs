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
    private float _rotationInLastFrame;

    protected void Awake()
    {
        InitializePositionAndRotation();

        SetVisionFunctionToDefault();

        ApplicationController.AddNetObject(this);
    }

    public void ResetMessages()
    {
        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;

        _sendMessageUpdateEvent = null;
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

    public virtual void SendMessageUpdate(IConnectionMember address)
    {
        IfChangeSendPositionUpdate(address);

        IfChangeSendRotationUpdate(address);

        InvokeSendMessageUpdateEvent(address);
    }

    public virtual void SendMessageDisappeared(IConnectionMember address)
    {
        var request = new DeleteObject(IdNet);
        var message = new OutgoingMessage(request);
        Server.Send(message, address);
    }

    public void SendChangeHpMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            int hp = GetComponent<HealthSystem>().HP;
            var package = new UpdateHP(IdNet, hp);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendChangeMaxHpMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            int maxHp = GetComponent<HealthSystem>().MaxHP;
            var package = new UpdateMaxHP(IdNet, maxHp);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
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

    protected bool IsChangePosition()
    {
        return transform.position != _positionInLastFrame;
    }

    protected bool IsChangeRotation()
    {
        return transform.eulerAngles.y != _rotationInLastFrame;
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

    private bool DefaultVisionFunction(Vision vision)
    {
        return true;
    }
}

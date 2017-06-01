using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public abstract class NetObject : MonoBehaviour 
{
    public int IdNet { get; set; }
    public bool IsModelVisible { get; set; }

    protected Func<Vision, bool> _isVisible;

    protected event Action<IConnectionMember> _sendMessageUpdateEvent;

    private bool _wasMovement;
    private Vector3 _positionInLastFrame;
    private bool _wasRotation;
    private float _rotationInLastFrame;

    protected void Awake()
    {
        InitializePositionAndRotation();

        SetVisionFunctionToDefault();

        IsModelVisible = true;
    }

    public void Update()
    {
        CheckMovementAndRotation();
    }

    public void ResetMessages()
    {
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
        var request = new DeleteObjectToClient(IdNet);
        var message = new OutgoingMessage(request);
        Server.Send(message, address);
    }

    public void SendChangeHpMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            int hp = GetComponent<HealthSystem>().HP;
            var package = new UpdateHPToClient(IdNet, hp);
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
            var package = new UpdateMaxHPToClient(IdNet, maxHp);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendDeadMessage()
    {
        var request = new DeadToClient(IdNet);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    public void SendRespawnMessage()
    {
        var request = new RespawnToClient(IdNet, transform.position);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    public void SendVisibleModelMessage()
    {
        var request = new ChangeVisibilityModelToClient(IdNet, IsModelVisible);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    public void SendChangePosition()
    {
        var request = new NewPositionToClient(IdNet, transform.position);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    protected void SetVisionFunction(Func<Vision, bool> function)
    {
        _isVisible = function;
    }

    protected void IfChangeSendPositionUpdate(IConnectionMember address)
    {
        if (IsChangePosition())
        {
            var package = new MoveToClient(IdNet, transform.position);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        }
    }

    protected void IfChangeSendRotationUpdate(IConnectionMember address)
    {
        if (IsChangeRotation())
        {
            var package = new RotateToClient(IdNet, transform.eulerAngles.y);
            var message = new OutgoingMessage(package);

            Server.Send(message, address);
        }
    }

    protected bool IsChangePosition()
    {
        return _wasMovement;
    }

    protected bool IsChangeRotation()
    {
        return _wasRotation;
    }

    protected void InitializePositionAndRotation()
    {
        _wasMovement = false;
        _wasRotation = false;

        _positionInLastFrame = transform.position;
        _rotationInLastFrame = transform.eulerAngles.y;
    }

    protected void CheckMovementAndRotation()
    {
        _wasMovement = transform.position != _positionInLastFrame;
        _wasRotation = transform.eulerAngles.y != _rotationInLastFrame;

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

    protected void GenerateSendFunctionAndAddToUpdateEvent(INetworkRequestToClient request)
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            Server.SendRequestAsMessage(request, address);
        };

        _sendMessageUpdateEvent += function;
    }

    private bool DefaultVisionFunction(Vision vision)
    {
        return true;
    }
}

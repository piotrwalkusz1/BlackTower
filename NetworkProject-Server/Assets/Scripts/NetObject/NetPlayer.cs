using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class NetPlayer : NetNamedObject
{
    private event Action _messages;

    private bool _isJumpMessage;
    private bool _isJumpMessageInLastFrame;

    private bool _isAttackMessage;
    private bool _isAttackMessageInLastFrame;

    private bool _isAllStatsMessage;
    private bool _isAllStatsMessageInLastFrame;

    private bool _isEquipedItemMessage;
    private bool _isEquipedItemMessageInLastFrame;

    new protected void Awake()
    {
        VisionFunctionToDefault();

        InitializePositionAndRotation();
    }

    new protected void LateUpdate()
    {
        MessageFlagsToDefault();
    }

    public void Logout()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsMustUpdate()
    {
        return (base.IsMustUpdate() || _isJumpMessageInLastFrame || _isAttackMessageInLastFrame || _isAllStatsMessageInLastFrame || _isEquipedItemMessageInLastFrame);
    }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        if(Address.Equals(address))
        {
            return;
        }

        OtherPlayerPackage otherPlayer = GetOtherPlayerPackage();

        Server.SendMessageCreateOtherPlayer(otherPlayer, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        if (Address.Equals(address))
        {
            return;
        }

        IfChangeSendPositionUpdate(address);

        IfChangeSendRotationUpdate(address);

        if (!IfFlagSendAllStatsUpdate(address))
        {
            IfFlagSendHpUpdate(address);

            IfFlagSendMaxHpUpdate(address);
        }

        IfFlagSendJumpEvent(address);

        IfFlagSendAttackEvent(address);

        IfFlagSendEquipedItemMessage(address);
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        if (Address.Equals(address))
        {
            return;
        }

        Server.SendMessageDeleteObject(IdNet, address);
    }

    public void SendJumpMessage()
    {
        _message;
    }

    public void SendAttackMessage()
    {
        _isAttackMessage = true;
    }

    public void SendChangeAllStatsMessage()
    {
        _isAllStatsMessage = true;
    }

    public void SendUpdateEquipedItems()
    {
        _isEquipedItemMessage = true;
    }

    public bool IsDead()
    {
        return GetComponent<PlayerHealthSystem>().IsDead();
    }

    public OtherPlayerPackage GetOtherPlayerPackage()
    {
        var playerPackage = new OtherPlayerPackage();

        playerPackage.IdObject = IdNet;
        playerPackage.Name = Name;
        playerPackage.Position = transform.position;
        playerPackage.Rotation = transform.eulerAngles.y;
        playerPackage.Stats = GetComponent<PlayerStats>().GetOtherPlayerStatsPackage();

        return playerPackage;
    }

    public OwnPlayerPackage GetOwnPlayerPackage()
    {
        var playerPackage = new OwnPlayerPackage();

        playerPackage.IdObject = IdNet;
        playerPackage.Name = Name;
        playerPackage.Position = transform.position;
        playerPackage.Stats = GetComponent<PlayerStats>().GetOwnPlayerStatsPackage();

        return playerPackage;
    }

    public void InitializePlayer(int netId, RegisterCharacter characterData, IConnectionMember address)
    {
        IdNet = netId;
        Name = characterData.Name;

        var vision = GetComponent<Vision>();
        vision.AddObserver(address);

        var stats = GetComponent<PlayerStats>();
        stats.Set(characterData);      

        var spells = GetComponent<SpellCaster>();
        spells.SetSpells(characterData.Spells);

        var experience = GetComponent<PlayerExperience>();
        experience.Set(characterData.Lvl, characterData.Exp);

        GetComponent<PlayerEquipment>().AddItem(new Item(0)); // temporary
        GetComponent<PlayerEquipment>().AddItem(new Item(2)); // temporary

        stats.CalculateStats();

        GetComponent<PlayerHealthSystem>().Recuparate();
    }

    new protected void MessageFlagsToDefault()
    {
        base.MessageFlagsToDefault();

        _isAttackMessageInLastFrame = _isAttackMessage;
        _isAttackMessage = false;

        _isJumpMessageInLastFrame = _isJumpMessage;
        _isJumpMessage = false;

        _isAllStatsMessageInLastFrame = _isAllStatsMessage;
        _isAllStatsMessage = false;

        _isEquipedItemMessageInLastFrame = _isEquipedItemMessage;
        _isEquipedItemMessage = false;
    } 

    protected bool IfFlagSendAllStatsUpdate(IConnectionMember address)
    {
        if (_isAllStatsMessageInLastFrame)
        {
            var package = GetComponent<PlayerStats>().GetOtherPlayerStatsPackage();

            Server.SendMessageUpdateOtherAllStats(IdNet ,package, address);

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void IfFlagSendAttackEvent(IConnectionMember address)
    {
        if (_isAttackMessageInLastFrame)
        {
            Server.SendMessagePlayerAttack(IdNet, address);
        }
    }

    protected void IfFlagSendJumpEvent(IConnectionMember address)
    {
        if (_isJumpMessageInLastFrame)
        {
            Server.SendMessageJumpOtherPlayer(IdNet, transform.position, Vector3.zero, address);
        }
    }

    new protected void IfChangeSendPositionUpdate(IConnectionMember address)
    {
        if (IsChangePosition())
        {
            Server.SendMessageMoveOtherPlayer(IdNet, transform.position, address);
        }
    }

    new protected void IfChangeSendRotationUpdate(IConnectionMember address)
    {
        if (IsChangeRotation())
        {
            Server.SendMessageRotationOtherPlayer(IdNet, transform.eulerAngles.y, address);
        }
    }

    protected void IfFlagSendEquipedItemMessage(IConnectionMember address)
    {
        if (_isEquipedItemMessageInLastFrame)
        {
            var equipedItems = GetComponent<PlayerEquipment>().GetEquipedItems();

            Server.SendMessageUpdateOtherEquipedItems(IdNet, equipedItems, address);
        }
    }

    private void SendJumpMessageFunction()
    {
        var message = new NetworkProject.Connection.ToClient.Jump(IdNet);


    }
}

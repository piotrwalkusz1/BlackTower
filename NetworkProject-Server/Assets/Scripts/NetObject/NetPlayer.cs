using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.BodyParts;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public class NetPlayer : NetObject
{
    public IConnectionMember OwnerAddress { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        if(OwnerAddress.Equals(address))
        {
            return;
        }

        var stats = GetComponent<PlayerStats>();
        var request = new CreateOtherPlayer(IdNet, transform.position, transform.eulerAngles.y, stats);
        var message = new OutgoingMessage(request);

        Server.Send(message, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        if (OwnerAddress.Equals(address))
        {
            return;
        }

        base.SendMessageUpdate(address);
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        if (OwnerAddress.Equals(address))
        {
            return;
        }

        base.SendMessageDisappeared(address);
    }

    public void SendJumpMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new Jump(IdNet);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendAttackMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new Attack(IdNet);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendChangeAllStatsMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new UpdateAllStats(IdNet, GetComponent<PlayerStats>());
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendUpdateEquipedItem(BodyPartSlot slot, Item item)
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new UpdateEquipedItem(IdNet, slot, item);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class NetControlled : NetVisualObject
{
    public IConnectionMember ControllerAddress { get; set; }

    private bool _isUndoTakeOver = false;

    public override void SendMessageAppeared(IConnectionMember address)
    {
        if (!address.Equals(ControllerAddress))
        {
            base.SendMessageAppeared(address);
        }     
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        if (!address.Equals(ControllerAddress))
        {
            base.SendMessageUpdate(address);
        }       
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        base.SendMessageDisappeared(address);
    }

    void OnDead()
    {
        if(!_isUndoTakeOver)
        {
            SendTakeOverPlayerObject();

            _isUndoTakeOver = true;
        }
    }

    void OnDestroy()
    {
        if (!_isUndoTakeOver)
        {
            SendTakeOverPlayerObject();

            _isUndoTakeOver = true;
        }
    }

    public void SendCreateAndTakeOverToOwner()
    {
        base.SendMessageAppeared(ControllerAddress);

        SendTakeOverThisObject();
    }

    public void SendTakeOverThisObject()
    {
        var request = new TakeOverObjectToClient(IdNet);

        Server.SendRequestAsMessage(request, ControllerAddress);
    }

    private void SendTakeOverPlayerObject()
    {
        NetPlayer netPlayer = AccountRepository.FindNetPlayerByAddress(ControllerAddress);

        var request = new TakeOverObjectToClient(netPlayer.IdNet);

        Server.SendRequestAsMessage(request, ControllerAddress);
    } 
}

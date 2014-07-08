using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class NetMonster : NetObject
{
    public int IdMonster { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var receive = new CreateMonsterToClient(IdNet, transform.position, transform.eulerAngles.y, IdMonster, GetComponent<MonsterStats>());

        Server.SendRequestAsMessage(receive, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        IfChangeSendPositionUpdate(address);

        IfChangeSendRotationUpdate(address);

        InvokeSendMessageUpdateEvent(address);
    }

    public void SendJumpMessage()
    {
        var request = new JumpToClient(IdNet);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    public void SendAttackMessage(int idAttackVictim)
    {
        var request = new AttackToClient(IdNet);

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }

    public void SendAllStatsMessage()
    {
        var request = new UpdateAllStatsToClient(IdNet, (IStats)GetComponent(typeof(IStats)));

        GenerateSendFunctionAndAddToUpdateEvent(request);
    }
}

using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class PlayerHealthSystem : HealthSystem
{
    public int Defense { get; set; }

    public override void AttackWithoutSendUpdate(AttackInfo attackInfo)
    {
        if (!IsDead())
        {
            attackInfo = ApplyAttackEvent(attackInfo);

            int dmg = ApplyDefense(attackInfo.Dmg);

            DecreaseHP(dmg);
        }
    }

    public override void DieAndSendUpdate()
    {
        ChangeHp(0);

        if (!_isSendedDeadMessage)
        {
            NetPlayer player = GetComponent<NetPlayer>();

            player.SendDeadMessage();

            var request = new DeadToClient(player.IdNet);

            Server.SendRequestAsMessage(request, player.OwnerAddress);
        }  
    }

    public override void SendUpdateHP()
    {
        SendUpdateHPToOthers();

        SendUpdateHPToOwner();
    }

    public void SendUpdateHPToOwner()
    {
        SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);

        NetPlayer netPlayer = GetComponent<NetPlayer>();

        var request = new UpdateHPToClient(netPlayer.IdNet, HP);

        Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
    }

    public void RecuperateAndSendHPUpdate()
    {
        Recuparate();

        SendUpdateHP();
    }

    protected int ApplyDefense(int baseDmg)
    {
        int dmg = baseDmg - Defense;

        if (dmg < 0)
        {
            dmg = 0;
        }

        return dmg;
    }
}

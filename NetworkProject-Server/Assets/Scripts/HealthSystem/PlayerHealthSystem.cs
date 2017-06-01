using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

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

    public void IncreaseHPAndSendUpdate(int value)
    {
        int oldHP = HP;

        IncreaseHP(value);

        if (oldHP != HP)
        {
            SendUpdateHP();
        }
    }

    public void DecreaseHPAndSendUpdate(int value)
    {
        int oldHP = HP;

        DecreaseHP(value);

        if (oldHP != HP)
        {
            SendUpdateHP();
        }
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

    protected override void Die()
    {
        ChangeHp(0);
    }

    protected override KillInfo GetKillInfo()
    {
        var address = GetComponent<NetPlayer>().OwnerAddress;

        return new PlayerKillInfo(AccountRepository.GetOnlineCharacterByAddress(address).IdCharacter);
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

using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerHealthSystem : HealthSystem
{
    public int Defense { get; set; }

    public override void Attack(AttackInfo attackInfo)
    {
        attackInfo = ApplyAttackEvent(attackInfo);

        int dmg = ApplyDefense(attackInfo.Dmg);

        DecreaseHP(dmg);
    }

    public override void Die()
    {
        NetPlayer player = GetComponent<NetPlayer>();

        OwnDeadEventPackage deadInfo = new OwnDeadEventPackage();

        Server.SendMessageYouAreDead(deadInfo, player.Address);
    }

    public void SendHpUpdatingToOwner()
    {
        NetPlayer netPlayer = GetComponent<NetPlayer>();

        Server.SendMessageUpdateYourHp(HP, netPlayer.Address);
    }

    public void SendHpUpdatingToOtherPlayer()
    {
        NetPlayer netPlayer = GetComponent<NetPlayer>();

        netPlayer.SendChangeHpMessage();
    }

    public override void SendHpUpdating()
    {
        NetPlayer netPlayer = GetComponent<NetPlayer>();

        netPlayer.SendChangeHpMessage();

        Server.SendMessageUpdateYourHp(HP, netPlayer.Address);
    }

    public void RecuperateAndSendHpUpdating()
    {
        Recuparate();

        SendHpUpdating();
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

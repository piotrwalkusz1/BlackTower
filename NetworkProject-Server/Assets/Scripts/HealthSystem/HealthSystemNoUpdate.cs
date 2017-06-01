using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HealthSystemNoUpdate : HealthSystem
{
    public override void AttackAndSendUpdate(AttackInfo attackInfo)
    {
        AttackWithoutSendUpdate(attackInfo);
    }

    public override void DieAndSendUpdate()
    {
        Die();
    }

    public override void SendUpdateHP()
    {
        
    }

    protected override KillInfo GetKillInfo()
    {
        return new EmptyKillInfo();
    }
}

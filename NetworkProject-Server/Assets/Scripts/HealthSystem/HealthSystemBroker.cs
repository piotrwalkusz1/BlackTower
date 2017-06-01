using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HealthSystemBroker : HealthSystemBase
{
    public HealthSystemBase _mainHealthSystem;

    public override void AttackWithoutSendUpdate(AttackInfo attackInfo)
    {
        _mainHealthSystem.AttackWithoutSendUpdate(attackInfo);
    }

    public override void AttackAndSendUpdate(AttackInfo attackInfo)
    {
        _mainHealthSystem.AttackAndSendUpdate(attackInfo);
    }

    public override void DieAndSendUpdate()
    {
        _mainHealthSystem.DieAndSendUpdate();
    }
}

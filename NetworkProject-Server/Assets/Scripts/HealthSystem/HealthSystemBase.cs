using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class HealthSystemBase : MonoBehaviour
{
    public abstract void AttackWithoutSendUpdate(AttackInfo attackInfo);

    public abstract void AttackAndSendUpdate(AttackInfo attackInfo);

    public abstract void DieAndSendUpdate();
}

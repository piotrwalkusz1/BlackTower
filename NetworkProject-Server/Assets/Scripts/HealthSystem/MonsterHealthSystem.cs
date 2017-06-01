using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MonsterHealthSystem : HealthSystem
{
    protected override KillInfo GetKillInfo()
    {
        return new MonsterKillInfo(GetComponent<NetMonster>().IdMonster);
    }
}

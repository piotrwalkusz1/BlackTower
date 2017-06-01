using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MonsterKillInfo : KillInfo
{
    public int IdMonster { get; set; }

    public MonsterKillInfo(int idMonster)
    {
        IdMonster = idMonster;
    }
}

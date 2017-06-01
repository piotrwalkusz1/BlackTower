using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;
using Standard;

public class KillQuestTarget : IQuestTarget
{
    public int KillTarget { get; set; }
    public int MonsterId { get; set; }
    public int AlreadyKill { get; set; }

    public string MonsterName
    {
        get { return Languages.GetMonsterName(MonsterId); }
    }

    public KillQuestTarget(int killTarget, int monsterId, int alreadyKill)
    {
        KillTarget = killTarget;
        MonsterId = monsterId;
        AlreadyKill = alreadyKill;
    }

    public string GetDescription()
    {
        string description = "";

        description += Languages.GetPhrase("killQuestDescription") + MonsterName + " : ";

        description += AlreadyKill.ToString() + "/" + KillTarget.ToString();

        return description;
    }

    public void Reset()
    {
        AlreadyKill = 0;
    }
}

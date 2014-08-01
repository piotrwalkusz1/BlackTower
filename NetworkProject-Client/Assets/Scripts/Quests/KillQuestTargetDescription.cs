using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;
using Standard;

public class KillQuestTargetDescription : KillQuestTarget, IQuestTargetDescripted
{
    public string MonsterName
    {
        get { return Languages.GetMonsterName(MonsterId); }
    }

    public KillQuestTargetDescription(int targetKill, int monsterId)
        : base(targetKill, monsterId)
    {

    }

    public string GetDescription()
    {
        string description = "";

        description += Languages.GetPhrase("killQuestDescription");

        description += AlreadyKill.ToString() + "/" + TargetKill.ToString();

        return description;
    }
}

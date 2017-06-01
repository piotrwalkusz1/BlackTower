using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public class KillQuestTargetData : IQuestTargetData
{
    public int KillTarget { get; set; }
    public int MonsterId { get; set; }

    public KillQuestTargetData(int killTarget, int monsterId)
    {
        KillTarget = killTarget;
        MonsterId = monsterId;
    }

    public IQuestTarget GetExecutedVersionTarget(Quest quest)
    {
        return new KillQuestTarget(KillTarget, MonsterId, quest);
    }

    public IQuestTargetPackage ToPackage()
    {
        return new KillQuestTargetPackage(KillTarget, MonsterId, 0);
    }
}

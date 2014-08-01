using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public class KillQuestTargetSystem : KillQuestTarget, IQuestTargetSystem
{
    public KillQuestTargetSystem(int killTarget, int monsterId)
        : base(killTarget, monsterId)
    {
            
    }

    public void Initialize(GameObject player)
    {
        throw new NotImplementedException();
    }
}

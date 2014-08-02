using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public class KillQuestTargetSystem : KillQuestTarget, IQuestTargetSystem
{
    private Combat _combatToDispose;

    public KillQuestTargetSystem(int killTarget, int monsterId)
        : base(killTarget, monsterId)
    {
            
    }

    public void Initialize(GameObject player)
    {
        Combat combat = player.GetComponent<Combat>();

        combat.OnKill += OnKill;

        _combatToDispose = combat;
    }

    public void Dispose()
    {
        _combatToDispose.OnKill -= OnKill;
    }

    private void OnKill(KillInfo killInfo)
    {
        if (!IsComplete() && killInfo is MonsterKillInfo)
        {
            var monsterKillInfo = (MonsterKillInfo)killInfo;

            if (monsterKillInfo.IdMonster == MonsterId)
            {
                AlreadyKill++;
            }
        }
    }
}

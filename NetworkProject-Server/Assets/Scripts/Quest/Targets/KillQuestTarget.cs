using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;
using NetworkProject.Connection.ToClient;

[Serializable]
public class KillQuestTarget : IQuestTarget
{
    public int TargetKill { get; set; }
    public int MonsterId { get; set; }
    public int AlreadyKill { get; set; }

    public Quest Quest { get; set; }

    public KillQuestTarget(int killTarget, int monsterId, Quest quest)
    {
        TargetKill = killTarget;
        MonsterId = monsterId;       
        Quest = quest;

        AlreadyKill = 0;
    }

    public KillQuestTarget(int killTarget, int monsterId, int alreadyKill, Quest quest)
    {
        TargetKill = killTarget;
        MonsterId = monsterId;
        AlreadyKill = alreadyKill;
        Quest = quest;
    }

    public void Initialize(GameObject player)
    {
        Combat combat = player.GetComponent<Combat>();

        combat.OnKill += OnKill;
    }

    public void Dispose(GameObject player)
    {
        player.GetComponent<Combat>().OnKill -= OnKill;
    }

    private void OnKill(KillInfo killInfo, GameObject killer)
    {
        if (!IsComplete() && killInfo is MonsterKillInfo)
        {
            var monsterKillInfo = (MonsterKillInfo)killInfo;

            if (monsterKillInfo.IdMonster == MonsterId)
            {
                AlreadyKill++;
            }

            var request = new KillQuestUpdateToClient(Quest.IdQuest, Quest.GetTargetId(this), AlreadyKill);

            Server.SendRequestAsMessage(request, killer.GetComponent<NetPlayer>().OwnerAddress);
        }
    }

    public IQuestTargetPackage ToPackage()
    {
        return new KillQuestTargetPackage(TargetKill, MonsterId, AlreadyKill);
    }


    public bool IsComplete()
    {
        return AlreadyKill >= TargetKill;
    }
}

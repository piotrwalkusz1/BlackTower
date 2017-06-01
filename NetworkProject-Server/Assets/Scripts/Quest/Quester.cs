using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Quests;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class 
    Quester : MonoBehaviour
{
    public List<int> _idQuestsToGive;
    public List<int> _idQuestsToReturn;

    public bool CanGiveQuest(QuestExecutor questExecutor, int idQuest)
    {
        var quest = QuestRepository.GetQuest(idQuest);
        var stats = GetComponent<PlayerStats>();

        Vector3 offset = questExecutor.transform.position - transform.position;

        if (offset.sqrMagnitude > Settings.talkNPCRange * Settings.talkNPCRange)
        {
            MonoBehaviour.print("NPC jest za daleko.");
            return false;
        }
        else if (questExecutor.IsQuestReturned(idQuest))
        {
            MonoBehaviour.print("Quest został już wykonany.");
            return false;
        }
        else if (questExecutor.IsQuestExecuting(idQuest))
        {
            MonoBehaviour.print("Quest jest obecnie wykonywany.");
            return false;
        }
        else if (!_idQuestsToGive.Exists(x => x == idQuest))
        {
            MonoBehaviour.print("Quester nie ma takiego questa.");
            return false;
        }
        else if(!quest.AreRequirementsSatisfy(stats))
        {
            MonoBehaviour.print("Wymagania nie zostały spełnione.");
            return false;
        }
        else if(!quest.AreAllRequiredCompletedQuest(questExecutor.GetReturnedQuest().ToList()))
        {
            MonoBehaviour.print("Nie ukończono wszystkich wymaganych questów.");
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public bool CanReturnQuest(QuestExecutor questExecutor, int idQuest)
    {
        if (!questExecutor.IsQuestCompleted(idQuest))
        {
            MonoBehaviour.print("Quest jest nieukończony.");
            return false;
        }

        if (!questExecutor.CanTakeRewardForQuest(idQuest))
        {
            MonoBehaviour.print("Nie można wziąć nagrody.");
            return false;
        }

        Vector3 offset = transform.position - questExecutor.transform.position;

        if (offset.sqrMagnitude > NetworkProject.Settings.talkNPCRange * NetworkProject.Settings.talkNPCRange)
        {
            MonoBehaviour.print("Za daleko, aby oddać quest.");
            return false;
        }

        if (!_idQuestsToReturn.Exists(x => x == idQuest))
        {
            MonoBehaviour.print("U tego questera nie można oddać tego questa.");
            return false;
        }

        return true;
    }

    public void GiveQuestAndSendMessage(int idQuest, QuestExecutor executor, IConnectionMember address)
    {
        int idNet = GetComponent<NetObject>().IdNet;

        executor.TakeQuest(idQuest, idNet);

        var request = new GiveQuestToClient(idQuest);

        Server.SendRequestAsMessage(request, address);
    }

    public void GiveQuest(int idQuest, QuestExecutor executor)
    {
        executor.TakeQuest(idQuest, GetComponent<NetObject>().IdNet);
    }

    public void TryGiveRewardForQuest(QuestExecutor questExecutor, int idQuest)
    {
        if (CanReturnQuest(questExecutor, idQuest))
        {
            Quest quest = questExecutor.GetCurrentQuest(idQuest);

            questExecutor.ReturnQuest(idQuest);  
        }  
    }
}

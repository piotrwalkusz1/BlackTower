using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[Serializable]
public class Quest
{
    public int IdQuest { get; set; }
    public int NetIdQuester { get; set; }

    public QuestData QuestData
    {
        get
        {
            return QuestRepository.GetQuest(IdQuest);
        }
    }

    protected bool _isCompleteMessageSended = false;

    protected List<IQuestTarget> _targets;

    public Quest(int idQuest, int netIdQuester)
    {
        IdQuest = idQuest;
        NetIdQuester = netIdQuester;

        _targets = QuestRepository.GetQuest(IdQuest).GetExecutedVersionTargets(this);
    }     

    public IQuestTarget[] GetTargets()
    {
        return _targets.ToArray();
    }

    public void Initialize(GameObject questExecutor)
    {
        foreach (var target in _targets)
        {
            target.Initialize(questExecutor);
        }
    }

    public void Dispose(GameObject player)
    {
        foreach (var target in _targets)
        {
            target.Dispose(player);
        }
    }

    public void IsCompleteSendMessage(IConnectionMember address)
    {
        if (!_isCompleteMessageSended && IsComplete())
        {
            var request = new QuestCompletedToClient(IdQuest);

            Server.SendRequestAsMessage(request, address);

            _isCompleteMessageSended = true;
        }
    }

    public bool IsComplete()
    {
        foreach (var tartget in _targets)
        {
            if (!tartget.IsComplete())
            {
                return false;
            }
        }

        return true;
    }

    public int GetTargetId(IQuestTarget target)
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i] == target)
            {
                return i;
            }
        }

        throw new ArgumentException("Nie ma takiego targetu w tym quescie.");
    }

    public bool AreAllRequiredCompletedQuest(List<int> completedQuest)
    {
        return QuestData.AreAllRequiredCompletedQuest(completedQuest);
    }
}

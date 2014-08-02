using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public class QuestManager : MonoBehaviour
{
    private List<QuestSystem> _quests = new List<QuestSystem>();

    public void AddQuest(QuestSystem quest)
    {
        _quests.Add(quest);

        quest.Initialize(gameObject);
    }

    public void DeleteQuest(QuestSystem quest)
    {
        _quests.Remove(quest);

        quest.Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public class PlayerAdditionalData : MonoBehaviour
{
    private static List<IQuestTargetDescripted> _quests;

    private void Awake()
    {
        _quests = new List<IQuestTargetDescripted>();
    }

    public void AddQuest(IQuestTargetDescripted quest)
    {
        _quests.Add(quest);
    }
}

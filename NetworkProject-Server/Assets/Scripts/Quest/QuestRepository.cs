using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public static class QuestRepository
{
    public static List<QuestData> _quests = new List<QuestData>();

    static QuestRepository()
    {
        // idź do instruktora po broń
        QuestData quest0 = new QuestData(
            idQuest: 0,           
            targets: new IQuestTargetData[] { },
            requirements: new IRequirement[] { },
            rewards: new IReward[] { new ItemReward(new Item(0)) },
            requiredCompletedQuests: new int[] { });
        _quests.Add(quest0);

        // zabij kilka jumerów
        QuestData quest1 = new QuestData(
            idQuest: 1,
            targets: new IQuestTargetData[] { new KillQuestTargetData(5, 0) },
            requirements: new IRequirement[] { },
            rewards: new IReward[] { new ExpReward(100), new GoldReward(500) },
            requiredCompletedQuests: new int[] { 0 });
        _quests.Add(quest1);

        // idź do maga
        QuestData quest2 = new QuestData(
            idQuest: 2,
            targets: new IQuestTargetData[] { },
            requirements: new IRequirement[] { },
            rewards: new IReward[] { new ExpReward(30), new ItemReward(new ItemTalisman(0)) },
            requiredCompletedQuests: new int[] { 1 });
        _quests.Add(quest2);

        // idź do dowódcy odziały na pierwszej mapie
        QuestData quest3 = new QuestData(
            idQuest: 3,
            targets: new IQuestTargetData[] { },
            requirements: new IRequirement[] { },
            rewards: new IReward[] { },
            requiredCompletedQuests: new int[] { 2 });
        _quests.Add(quest3);

        // zabij potwory na pierwszym piętrze
        QuestData quest4 = new QuestData(
            idQuest: 4,
            targets: new IQuestTargetData[] { new KillQuestTargetData(10, 0), new KillQuestTargetData(5, 1),
                new KillQuestTargetData(1, 2) },
            requirements: new IRequirement[] { },
            rewards: new IReward[] { new ExpReward(250), new GoldReward(1000) },
            requiredCompletedQuests: new int[] { });
        _quests.Add(quest4);
    }

    public static QuestData GetQuest(int idQuest)
    {
        return _quests[idQuest];
    }
}

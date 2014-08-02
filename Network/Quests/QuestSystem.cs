using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Quests
{
    public class QuestSystem : Quest
    {
        public QuestSystem(IQuestTargetSystem[] targets, IReward[] rewards)
            : base(targets.Cast<IQuestTarget>().ToArray(), rewards)
        {

        }

        public void Initialize(GameObject player)
        {
            foreach (var target in _targets.Cast<IQuestTargetSystem>())
            {
                target.Initialize(player);
            }
        }

        public void Dispose()
        {
            foreach (var target in _targets.Cast<IQuestTargetSystem>())
            {
                target.Dispose();
            }
        }
    }
}

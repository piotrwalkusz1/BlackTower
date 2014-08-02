using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Quests
{
    public interface IQuestTargetSystem
    {
        void Initialize(GameObject player);

        void Dispose();

        IQuestTarget GetBaseQuestTarget();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public interface IQuestTarget
{
    void Initialize(GameObject player);

    void Dispose(GameObject player);

    bool IsComplete();

    IQuestTargetPackage ToPackage();
}

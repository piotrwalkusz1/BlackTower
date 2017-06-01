using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public interface IQuestTargetData
{
    IQuestTarget GetExecutedVersionTarget(Quest quest);

    IQuestTargetPackage ToPackage();
}

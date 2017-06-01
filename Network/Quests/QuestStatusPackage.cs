using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    [Serializable]
    public enum QuestStatusPackage
    {
        NoTaken,
        InProgress,
        Completed,
        Returned
    }
}

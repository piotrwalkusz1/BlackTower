using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    [Serializable]
    public class KillQuestTargetPackage : IQuestTargetPackage
    {
        public int MonsterId { get; protected set; }
        public int AlreadyKill { get; set; }
        public int TargetKill { get; protected set; }

        public KillQuestTargetPackage(int targetKill, int monsterId, int alreadyKill)
        {
            TargetKill = targetKill;
            MonsterId = monsterId;
            AlreadyKill = alreadyKill;
        }
    }
}

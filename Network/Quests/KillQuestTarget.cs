using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    public class KillQuestTarget : IQuestTarget
    {
        public int MonsterId { get; protected set; }
        public int AlreadyKill { get; protected set; }
        public int TargetKill { get; protected set; }

        public KillQuestTarget(int targertKill, int monsterId)
        {
            TargetKill = targertKill;
            MonsterId = monsterId;

            AlreadyKill = 0;
        }

        public KillQuestTarget(KillQuestTarget questTarget)
        {
            AlreadyKill = questTarget.AlreadyKill;
            TargetKill = questTarget.TargetKill;
            MonsterId = questTarget.MonsterId;
        }

        public bool IsComplete()
        {
            return AlreadyKill >= TargetKill;
        }

        public IQuestTarget GetCopy()
        {
            return new KillQuestTarget(this);
        }
    }
}

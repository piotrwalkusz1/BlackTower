using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class KillQuestUpdateToClient : QuestUpdateToClient
    {
        public int Kills { get; set; }

        public KillQuestUpdateToClient(int questId, int targetId, int kills)
        {
            QuestId = questId;
            TargetId = targetId;
            Kills = kills;
        }
    }
}

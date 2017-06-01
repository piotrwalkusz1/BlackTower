using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class TakeQuestToServer : INetworkRequestToServer
    {
        public int IdQuest { get; set; }
        public int IdQuester { get; set; }

        public TakeQuestToServer(int idQuest, int idQuester)
        {
            IdQuest = idQuest;
            IdQuester = idQuester;
        }
    }
}

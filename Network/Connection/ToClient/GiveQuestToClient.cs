using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GiveQuestToClient : INetworkRequestToClient
    {
        public int IdQuest { get; set; }

        public GiveQuestToClient(int idQuest)
        {
            IdQuest = idQuest;
        }
    }
}

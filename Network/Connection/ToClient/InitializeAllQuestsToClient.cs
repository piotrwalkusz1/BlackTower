using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class InitializeAllQuestsToClient : INetworkRequestToClient
    {
        public List<QuestDataPackage> Quests { get; set; }

        public InitializeAllQuestsToClient(QuestDataPackage[] quests)
        {
            Quests = new List<QuestDataPackage>(quests);
        }
    }
}

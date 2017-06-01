using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GiveQuestProposalToClient : INetworkRequestToClient
    {
        public QuestDataPackage Quest { get; set; }
        public int NetIdQuester { get; set; }

        public GiveQuestProposalToClient(QuestDataPackage quest, int netIdQuester)
        {
            Quest = quest;
            NetIdQuester = netIdQuester;
        }
    }
}

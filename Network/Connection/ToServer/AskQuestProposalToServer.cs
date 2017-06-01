using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class AskQuestProposalToServer : INetworkRequestToServer
    {
        public int IdQuest { get; set; }
        public int NetIdQuster { get; set; }

        public AskQuestProposalToServer(int idQuest, int netIdQuester)
        {
            IdQuest = idQuest;
            NetIdQuster = netIdQuester;
        }
    }
}

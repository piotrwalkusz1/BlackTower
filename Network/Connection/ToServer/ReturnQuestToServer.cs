using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ReturnQuestToServer : INetworkRequestToServer
    {
        public int IdQuest { get; set; }
        public int IdNetQuester { get; set; }

        public ReturnQuestToServer(int idQuest, int idNetQuester)
        {
            IdQuest = idQuest;
            IdNetQuester = idNetQuester;
        }
    }
}

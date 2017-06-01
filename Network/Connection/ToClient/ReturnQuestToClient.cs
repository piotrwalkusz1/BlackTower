using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ReturnQuestToClient : INetworkRequestToClient
    {
        public int IdQuest { get; set; }

        public ReturnQuestToClient(int idQuest)
        {
            IdQuest = idQuest;
        }
    }
}

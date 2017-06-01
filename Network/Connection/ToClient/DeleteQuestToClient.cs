using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class DeleteQuestToClient : INetworkRequestToClient
    {
        public int IdQuest { get; set; }

        public DeleteQuestToClient(int idQuest)
        {
            IdQuest = idQuest;
        }
    }
}

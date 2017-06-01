using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class QuestCompletedToClient : INetworkRequestToClient
    {
        public int QuestId { get; set; }

        public QuestCompletedToClient(int questId)
        {
            QuestId = questId;
        }
    }
}

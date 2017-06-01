using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public abstract class QuestUpdateToClient : INetworkRequestToClient
    {
        public int QuestId { get; set; }
        public int TargetId { get; set; }
    }
}

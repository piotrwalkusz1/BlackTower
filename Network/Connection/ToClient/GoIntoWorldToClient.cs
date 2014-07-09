using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GoIntoWorldToClient : INetworkRequestToClient
    {
        public int MapNumber { get; set; }

        public GoIntoWorldToClient(int mapNumber)
        {
            MapNumber = mapNumber;
        }
    }
}

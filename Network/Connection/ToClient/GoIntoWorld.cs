using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GoIntoWorld : INetworkRequest
    {
        public int MapNumber { get; set; }

        public GoIntoWorld(int mapNumber)
        {
            MapNumber = mapNumber;
        }
    }
}

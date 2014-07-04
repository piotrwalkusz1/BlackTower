using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PickItem : INetworkPackage
    {
        public int IdNetItem { get; set; }

        public PickItem(int idNetItem)
        {
            IdNetItem = idNetItem;
        }
    }
}

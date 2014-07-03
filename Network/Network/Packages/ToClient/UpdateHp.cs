using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class UpdateHP : INetworkPackage
    {
        public int NetId { get; set; }
        public int HP { get; set; }

        public UpdateHP(int netId, int hp)
        {
            NetId = netId;
            HP = hp;
        }
    }
}

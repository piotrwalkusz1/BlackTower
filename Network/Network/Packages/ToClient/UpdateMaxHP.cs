using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class UpdateMaxHP : INetworkPackage
    {
        public int IdNet { get; set; }
        public int MaxHP { get; set; }

        public UpdateMaxHP(int idNet, int maxHp)
        {
            IdNet = idNet;
            maxHp = MaxHP;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class OpenShopToServer : INetworkRequestToServer
    {
        public int NetIdMerchant { get; set; }

        public OpenShopToServer(int netIdMerchant)
        {
            NetIdMerchant = netIdMerchant;
        }
    }
}

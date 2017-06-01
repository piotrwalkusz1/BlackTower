using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class OpenShopToClient : INetworkRequestToClient
    {
        public ShopPackage Shop { get; set; }
        public int IdNetMerchant { get; set; }

        public OpenShopToClient(ShopPackage shop, int idNetMerchant)
        {
            Shop = shop;
            IdNetMerchant = idNetMerchant;
        }
    }
}

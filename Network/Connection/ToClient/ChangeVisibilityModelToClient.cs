using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ChangeVisibilityModelToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public bool IsVisibleModel { get; set; }

        public ChangeVisibilityModelToClient(int idNet, bool isVisibleModel)
        {
            IdNet = idNet;
            IsVisibleModel = isVisibleModel;
        }
    }
}

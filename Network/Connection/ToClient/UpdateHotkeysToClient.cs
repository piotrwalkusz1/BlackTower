using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateHotkeysToClient : INetworkRequestToClient
    {
        public List<HotkeysPackage> Hotkeys { get; set; }

        public UpdateHotkeysToClient(HotkeysPackage[] hotkeys)
        {
            Hotkeys = new List<HotkeysPackage>(hotkeys);
        }
    }
}

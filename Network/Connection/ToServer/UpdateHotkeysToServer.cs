using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class UpdateHotkeysToServer : INetworkRequestToServer
    {
        public List<HotkeysPackage> Hotkeys { get; set; }

        public UpdateHotkeysToServer(HotkeysPackage[] hotkeys)
        {
            Hotkeys = new List<HotkeysPackage>(hotkeys);
        }
    }
}

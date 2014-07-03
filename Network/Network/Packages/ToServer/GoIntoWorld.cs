using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class GoIntoWorld : INetworkPackage
    {
        public int CharacterSlot { get; set; }

        public GoIntoWorld(int characterSlot)
        {
            CharacterSlot = characterSlot;
        }
    }
}

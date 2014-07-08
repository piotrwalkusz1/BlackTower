using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class GoIntoWorldToServer : INetworkRequest
    {
        public int CharacterSlot { get; set; }

        public GoIntoWorldToServer(int characterSlot)
        {
            CharacterSlot = characterSlot;
        }
    }
}

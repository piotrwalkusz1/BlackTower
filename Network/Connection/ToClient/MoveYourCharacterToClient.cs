using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class MoveYourCharacterToClient : INetworkRequestToClient
    {
        public Vector3Serializable NewPosition { get; set; }

        public MoveYourCharacterToClient(Vector3Serializable newPosition)
        {
            NewPosition = newPosition;
        }
    }
}

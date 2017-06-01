using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkProject;

namespace UnitTest.FakeToServerTest
{
    class FakeGameObjectRepository : IGameObjectRepository
    {
        public NetItem[] GetNetItems()
        {
            throw new NotImplementedException();
        }

        public NetPlayer[] GetNetPlayers()
        {
            throw new NotImplementedException();
        }

        public PlayerRespawn[] GetPlayerRespawns()
        {
            throw new NotImplementedException();
        }

        public NetObject[] GetNetObjects()
        {
            throw new NotImplementedException();
        }
    }
}

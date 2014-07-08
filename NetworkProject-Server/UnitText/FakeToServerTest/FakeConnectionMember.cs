using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NetworkProject.Connection;

namespace UnitTest.FakeToServerTest
{
    class FakeConnectionMember : IConnectionMember
    {
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return new IPEndPoint(0x12432343, 3213);
            }
        }

        public bool Equals(IConnectionMember connectionMember)
        {
            return RemoteEndPoint.Equals(connectionMember.RemoteEndPoint);
        }
    }
}

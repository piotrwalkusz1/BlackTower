using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkProject;
using NetworkProject.Connection;

namespace UnitTest.FakeToServerTest
{
    class FakeServer : IServer
    {
        public ServerStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        public void Start(ServerConfig config)
        {

        }

        public void Send(OutgoingMessage message, IConnectionMember address)
        {
            ServerTest._sendedMessages.Add(message);
        }

        public IncomingMessage ReadMessage()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToServer;
using NetworkProject.Connection.ToClient;
using UnitTest.FakeToServerTest;

namespace UnitTest
{
    [TestClass]
    public class ServerTest
    {
        public static List<OutgoingMessage> _sendedMessages;

        private static IConnectionMember _address = new FakeConnectionMember();

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _sendedMessages = new List<OutgoingMessage>();
            
            Server.Set(new FakeServer());
            AccountRepository.Set(new FakeAccountRepository());
            GameObjectRepository.Set(new FakeGameObjectRepository());
        }

        [TestMethod]
        public void ReceiveLogin_Success()
        {
            var request = new LoginToGame("Login", "Password");
            IncomingMessage message = new IncomingMessage(request, _address);

            Server.ExecuteMessage(message);

            var sendedRequest = (GoToChoiceCharacterMenuToClient)_sendedMessages[0].Request;
            Assert.IsNotNull(AccountRepository.GetOnlineAccountByAddress(_address));
            Assert.AreEqual(1, _sendedMessages.Count);           
            Assert.AreEqual(1, sendedRequest.Characters.Count);
            Assert.AreEqual("Neon", sendedRequest.Characters[0].Name);
        }

        public static void LoginAccount()
        {
            AccountRepository.LoginAccount("Login", "Password", _address);
        }

        public static void LoginCharacter()
        {
            OnlineAccount onlineAccount = AccountRepository.GetOnlineAccountByAddress(_address);

            AccountRepository.LoginCharacter(onlineAccount, 0);
        }
    }
}

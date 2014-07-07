using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetworkProject;
using NetworkProject.Connection;

namespace UnitText
{
    [TestClass]
    public class ServerTest
    {
        private static OutgoingMessage _lastSendedMessage;

        private static RegisterAccount _registerAccount;
        private static RegisterCharacter _registerCharacter;
        private static OnlineAccount _onlineAccount;
        private static OnlineCharacter _onlineCharacter;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Server.Set(GetFakeServer());
            AccountRepository.Set(GetFakeAccountRepository());
            GameObjectRepository.Set(GetFakeGameObjectsRepository());

            _lastSendedMessage = null;
            _onlineAccount = null;
            _onlineCharacter = null;

            InitializeAccounts();
        }

        [TestMethod]
        public void ReceiveLogin_Success()
        {
            var request = new NetworkProject.Connection.ToServer.LoginToGame("login", "password");
            IncomingMessage message = new IncomingMessage(request, GetFakeConnectionMember());

            Server.ExecuteMessage(message);

            Assert.IsNotNull(_onlineAccount);
            var request2 = (NetworkProject.Connection.ToClient.GoToChoiceCharacterMenu)_lastSendedMessage.Request;
            Assert.AreEqual(request2.Characters.Count, 1);
            Assert.AreEqual(request2.Characters[0].Name, "Postac1");
        }

        public static IAccountRepository GetFakeAccountRepository()
        {
            var mock = new Mock<IAccountRepository>();

            mock.Setup(x => x.LoginAccount(It.Is<RegisterAccount>(y => y.IdAccount == 0), It.IsAny<IConnectionMember>()));
            mock.Setup(x => x.GetAccounts()).Returns(new RegisterAccount[] { _registerAccount });

            return mock.Object;
        }

        public static IGameObjectRepository GetFakeGameObjectsRepository()
        {
            var mock = new Mock<IGameObjectRepository>();

            return mock.Object;
        }

        public static IServer GetFakeServer()
        {
            var mock = new Mock<IServer>();

            mock.Setup(x => x.Send(It.IsAny<OutgoingMessage>(), It.IsAny<IConnectionMember>())).
                Callback((OutgoingMessage m, IConnectionMember a) => _lastSendedMessage = m);

            return mock.Object;
        }

        public static IConnectionMember GetFakeConnectionMember()
        {
            var mock = new Mock<IConnectionMember>();
            
            mock.SetupGet<IPEndPoint>(x => new IPEndPoint(0x2414188f, 3213));
            mock.Setup(x => x.Equals(It.IsAny<IConnectionMember>())).
                Returns<IConnectionMember>((IConnectionMember address) => address.RemoteEndPoint.Equals(address.RemoteEndPoint));

            return mock.Object;
        }

        public static void InitializeAccounts()
        {
            RegisterAccount account1 = new RegisterAccount("login", "password");
            account1.IdAccount = 0;

            RegisterCharacter character1 = new RegisterCharacter();
            character1.Name = "Postac1";
            character1.IdCharacter = 0;

            account1.AddCharacter(character1);

            _registerAccount = account1;
            _registerCharacter = character1;
        }

        public static void LoginAccount()
        {
            _onlineAccount = new OnlineAccount(0, GetFakeConnectionMember());
        }

        public static void LoginCharacter()
        {
            _onlineCharacter = _onlineAccount.LoginCharacter(0);
        }
    }
}

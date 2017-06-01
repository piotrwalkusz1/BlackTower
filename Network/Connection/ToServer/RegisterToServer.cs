using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class RegisterToServer : INetworkRequestToServer
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public RegisterToServer(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

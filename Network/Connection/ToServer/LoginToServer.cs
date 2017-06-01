using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class LoginToGame : INetworkRequestToServer
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginToGame(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class Register : INetworkPackage
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Register(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

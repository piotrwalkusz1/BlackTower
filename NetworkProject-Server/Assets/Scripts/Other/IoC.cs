using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

namespace Standard
{
    [System.CLSCompliant(false)]
    public static class IoC
    {
        public static void Initialize()
        {

        }

        public static IServer GetImplementationServer()
        {
            return new LidgrenServer();
        }

        public static IAccountsRepository GetImplementationAccountsRepository()
        {
            return new AccountsRepositoryInServer();
        }
    }
}



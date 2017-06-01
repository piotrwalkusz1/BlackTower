using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;

namespace Standard
{
    [System.CLSCompliant(false)]
    public static class IoC
    {
        public static void Initialize()
        {

        }

        public static IServer GetServer()
        {
            return new LidgrenServer();
        }

        public static IAccountRepository GetAccountRepository()
        {
            return new AccountRepositoryInServer();
        }

        public static IGameObjectRepository GetGameObjectRepository()
        {
            return new GameObjectRepositoryByFind();
        }
    }
}



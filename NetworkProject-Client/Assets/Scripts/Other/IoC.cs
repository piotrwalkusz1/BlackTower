using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace Standard
{
    public static class IoC
    {
        public static IClient GetClient()
        {
            return new LidgrenClient();
        }

        public static IGameObjectRepository GetGameObjectRepsitory()
        {
            return new GameObjectRepositoryByFind();
        }
    } 
}


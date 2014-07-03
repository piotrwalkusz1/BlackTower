﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class UseSpell : INetworkPackage
    {
        public int IdSpell { get; set; }

        public UseSpell(int idSpell)
        {
            IdSpell = idSpell;
        }
    }
}

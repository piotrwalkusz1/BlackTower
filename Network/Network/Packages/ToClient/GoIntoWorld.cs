﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class GoIntoWorld : INetworkPackage
    {
        public int MapNumber { get; set; }

        public GoIntoWorld(int mapNumber)
        {
            MapNumber = mapNumber;
        }
    }
}

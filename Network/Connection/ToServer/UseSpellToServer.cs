using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class UseSpellToServer : INetworkRequestToServer
    {
        public int IdSpell { get; set; }

        public UseSpellToServer(int idSpell)
        {
            IdSpell = idSpell;
        }
    }
}

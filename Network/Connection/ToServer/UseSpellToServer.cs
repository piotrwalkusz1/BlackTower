using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class UseSpellToServer : INetworkRequestToServer
    {
        public int IdSpell { get; set; }
        public ISpellCastOption[] Options { get; set; }

        public UseSpellToServer(int idSpell, params ISpellCastOption[] options)
        {
            IdSpell = idSpell;
            Options = options;
        }
    }
}

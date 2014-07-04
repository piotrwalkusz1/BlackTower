using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateAllSpells : INetworkPackage
    {
        public int IdNet { get; set; }
        public List<Spell> Spells { get; set; }

        public UpdateAllSpells(int idNet, List<Spell> spells)
        {
            IdNet = idNet;
            Spells = spells;
        }
    }
}

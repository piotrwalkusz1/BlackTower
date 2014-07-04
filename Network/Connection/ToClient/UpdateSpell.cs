using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateSpell : INetworkPackage
    {
        public int IdNet { get; set; }
        public Spell Spell { get; set; }

        public UpdateSpell(int idNet, Spell spell)
        {
            IdNet = idNet;
            Spell = spell;
        }
    }
}

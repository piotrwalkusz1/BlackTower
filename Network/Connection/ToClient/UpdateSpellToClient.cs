﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateSpellToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public SpellPackage Spell { get; set; }

        public UpdateSpellToClient(int idNet, SpellPackage spell)
        {
            IdNet = idNet;
            Spell = spell;
        }
    }
}

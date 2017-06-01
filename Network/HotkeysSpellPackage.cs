using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    [Serializable]
    public class HotkeysSpellPackage : HotkeysPackage
    {
        public int IdSpell { get; set; }

        public HotkeysSpellPackage(int idSpell)
        {
            IdSpell = idSpell;
        }
    }
}

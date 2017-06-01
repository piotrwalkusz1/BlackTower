using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    [Serializable]
    public class SpellCastOptionTargetObject : ISpellCastOption
    {
        public int NetIdTarget { get; set; }

        public SpellCastOptionTargetObject(int netIdTarget)
        {
            NetIdTarget = netIdTarget;
        }
    }
}

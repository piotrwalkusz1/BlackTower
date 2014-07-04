using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    [Serializable]
    public class Spell
    {
        public int IdSpell { get; set; }

        [NonSerialized]
        private DateTime _nextUseTime = DateTime.UtcNow;
    }
}

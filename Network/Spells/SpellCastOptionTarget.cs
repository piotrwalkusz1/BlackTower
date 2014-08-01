using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    public class SpellCastOptionTarget : ISpellCastOption
    {
        public ITarget Target { get; set; }

        public SpellCastOptionTarget(ITarget target)
        {
            Target = target;
        }
    }
}

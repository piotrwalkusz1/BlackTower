using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    [Serializable]
    public class SpellCastOptionTargetPosition : ISpellCastOption
    {
        public Vector3Serializable Position { get; set; }

        public SpellCastOptionTargetPosition(Vector3Serializable position)
        {
            Position = position;
        }
    }
}

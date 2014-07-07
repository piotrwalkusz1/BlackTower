using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    public interface ISpellRepository
    {
        SpellData GetSpell(int idSpell);
    }
}

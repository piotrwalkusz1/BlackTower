using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;

public class SpellRepositoryServer : ISpellRepository
{
    public List<SpellActionData> _spells;

    public SpellData GetSpell(int idSpell)
    {
        return _spells[idSpell];
    }
}

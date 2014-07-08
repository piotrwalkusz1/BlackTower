using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    public static class SpellRepository
    {
        private static ISpellRepository _repository;

        public static void Set(ISpellRepository repository)
        {
            _repository = repository;
        }

        public static SpellData GetSpell(int idSpell)
        {
            return _repository.GetSpell(idSpell);
        }
    }
}

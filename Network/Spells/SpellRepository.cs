using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Spells
{
    public static class SpellRepository
    {
        private static ISpellRepository _repository;

        static SpellRepository()
        {
            _repository = IoC.GetSpellRepository();
        }

        public static SpellData GetSpell(int idSpell)
        {
            return _repository.GetSpell(idSpell);
        }
    }
}

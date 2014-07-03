using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    public class Language
    {
        private Dictionary<string, string> _dictionary;

        public Language(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public string this[string name]
        {
            get
            {
                return _dictionary[name];
            }
        }

        public string GetItemName(int idItem)
        {
            return _dictionary["itemName" + idItem.ToString()];
        }

        public string GetSpellName(int idSpell)
        {
            return _dictionary["spellName" + idSpell.ToString()];
        }

        public string GetSpellDescription(int idSpell)
        {
            return _dictionary["spellDescription" + idSpell.ToString()];
        }
    }
}

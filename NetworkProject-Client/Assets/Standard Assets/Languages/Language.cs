using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    public class Language
    {
        private const string ITEM_NAME = "itemName";
        private const string SPELL_NAME = "spellName";
        private const string SPELL_DESCRIPTION = "spellDescription";
        private const string ERROR_TEXT = "errorText";

        public string Name { get; set; }

        private Dictionary<string, string> _dictionary;

        public Language(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    return _dictionary[name];
                }
                catch (ArgumentException)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    _dictionary[name] = value;
                }
                catch (ArgumentException)
                {
                    _dictionary.Add(name, value);
                }
            }
        }

        public string GetSentense(string name)
        {
            return this[name];
        }

        public string GetItemName(int idItem)
        {
            return this[ITEM_NAME + idItem.ToString()];
        }

        public string GetErrorText(int errorId)
        {
            return this[ERROR_TEXT + errorId.ToString()];
        }

        public string GetSpellDescription(int idSpell)
        {
            return this[SPELL_DESCRIPTION + idSpell.ToString()];
        }

        public string GetSpellName(int idSpell)
        {
            return this[SPELL_NAME + idSpell.ToString()];
        }

        public void SetItemName(int idItem, string value)
        {
            this[ITEM_NAME + idItem.ToString()] = value;
        }

        public void SetErrorText(int errorId, string value)
        {
            this[ERROR_TEXT + errorId.ToString()] = value;
        }

        public void SetSpellDescription(int idSpell, string value)
        {
            this[SPELL_DESCRIPTION + idSpell.ToString()] = value;
        }

        public void SetSpellName(int idSpell, string value)
        {
            this[SPELL_NAME + idSpell.ToString()] = value;
        }
    }
}

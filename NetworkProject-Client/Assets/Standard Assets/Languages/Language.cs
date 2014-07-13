using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    public class Language
    {
        public string Name { get; set; }

        public Dictionary<string, string> Pharses
        {
            get { return _dictionary; }
            private set { _dictionary = value; }
        }

        private Dictionary<string, string> _dictionary;

        public Language(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public Language(Dictionary<string, string> dictionary, string name)
        {
            _dictionary = dictionary;
            Name = name;
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

        public bool ContainPhrase(string phrase)
        {
            return _dictionary.ContainsKey(phrase);
        }

        //xml serializer require 0-argumnet constructor
        public Language()
        {
            _dictionary = new Dictionary<string, string>();
        }

        

        public string GetPhrase(string name)
        {
            return this[name];
        }

        public string GetItemName(int idItem)
        {
            return this[Languages.ITEM_NAME + idItem.ToString()];
        }

        public string GetErrorText(int errorId)
        {
            return this[Languages.ERROR_TEXT + errorId.ToString()];
        }

        public string GetSpellDescription(int idSpell)
        {
            return this[Languages.SPELL_DESCRIPTION + idSpell.ToString()];
        }

        public string GetSpellName(int idSpell)
        {
            return this[Languages.SPELL_NAME + idSpell.ToString()];
        }

        public void SetPhrase(string phrase, string value)
        {
            this[phrase] = value;
        }

        public void SetItemName(int idItem, string value)
        {
            this[Languages.ITEM_NAME + idItem.ToString()] = value;
        }

        public void SetErrorText(int errorId, string value)
        {
            this[Languages.ERROR_TEXT + errorId.ToString()] = value;
        }

        public void SetSpellDescription(int idSpell, string value)
        {
            this[Languages.SPELL_DESCRIPTION + idSpell.ToString()] = value;
        }

        public void SetSpellName(int idSpell, string value)
        {
            this[Languages.SPELL_NAME + idSpell.ToString()] = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
                catch (KeyNotFoundException)
                {
                    return "[" + name + "]";
                }
            }
            set
            {
                try
                {
                    _dictionary[name] = value;
                }
                catch (KeyNotFoundException)
                {
                    _dictionary.Add(name, value);
                }
            }
        }

        //xml serializer require 0-argumnet constructor
        public Language()
        {
            Name = "Language-NewLanguage";

            _dictionary = new Dictionary<string, string>();
        }

        public bool ContainPhrase(string phrase)
        {
            return _dictionary.ContainsKey(phrase);
        }

        public void DeletePhrase(string phrase)
        {
            _dictionary.Remove(phrase);
        }

        public string GetPhrase(string phrase)
        {
            return this[phrase];
        }

        public string GetItemName(int idItem)
        {
            return this[Languages.ITEM_NAME + idItem.ToString()];
        }

        public string GetMessageText(int idMessageText)
        {
            return this[Languages.MESSAGE_TEXT + idMessageText.ToString()];
        }

        public string GetSpellDescription(int idSpell)
        {
            return this[Languages.SPELL_DESCRIPTION + idSpell.ToString()];
        }

        public string GetSpellName(int idSpell)
        {
            return this[Languages.SPELL_NAME + idSpell.ToString()];
        }

        public string GetMonsterName(int idMonster)
        {
            return this[Languages.MONSTER_NAME + idMonster.ToString()];
        }

        public string GetQuestName(int idQuest)
        {
            return this[Languages.QUEST_NAME + idQuest.ToString()];
        }

        public string GetQuestDescription(int idQuest)
        {
            return this[Languages.QUEST_DESCRIPTION + idQuest.ToString()];
        }

        public string GetDialog(int idDialog)
        {
            return this[Languages.DIALOG + idDialog.ToString()];
        }

        public string GetBuffName(int idBuff)
        {
            return this[Languages.BUFF_NAME + idBuff.ToString()];
        }

        public string GetBuffDescription(int idBuff)
        {
            return this[Languages.BUFF_DESCRIPTION + idBuff.ToString()];
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
            this[Languages.MESSAGE_TEXT + errorId.ToString()] = value;
        }

        public void SetSpellDescription(int idSpell, string value)
        {
            this[Languages.SPELL_DESCRIPTION + idSpell.ToString()] = value;
        }

        public void SetSpellName(int idSpell, string value)
        {
            this[Languages.SPELL_NAME + idSpell.ToString()] = value;
        }

        public void UpdatePhrases(string[] phrases)
        {
            foreach (var phrase in phrases)
            {
                if (!ContainPhrase(phrase))
                {
                    SetPhrase(phrase, "");
                }
            }

            foreach(var phrase in _dictionary.Keys.ToArray())
            {
                if(!phrases.Contains(phrase))
                {
                    DeletePhrase(phrase);
                }
            }
        }
    }
}

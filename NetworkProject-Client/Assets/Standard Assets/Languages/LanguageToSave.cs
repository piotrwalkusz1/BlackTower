using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Standard
{
    public class LanguageToSave
    {
        public List<KeyValuePair<string, string>> _dictionary;
        public string _name;

        public LanguageToSave()
        {
            
        }

        public LanguageToSave(Language language)
        {
            _name = language.Name;

            _dictionary = new List<KeyValuePair<string, string>>();

            foreach (var item in language.Pharses)
            {
                _dictionary.Add(item);
            }
        }

        public Language GetLanguage()
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var item in _dictionary)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return new Language(dictionary, _name);
        }
    }
}

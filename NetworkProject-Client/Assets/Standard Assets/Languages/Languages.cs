using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

namespace Standard
{
    public static class Languages
    {
        private static Language _currentLanguage;

        static Languages()
        {
            SetLanguageByName("PL");
        }

        public static string GetSentence(string name)
        {
            return _currentLanguage[name];
        }

        public static string GetItemName(int idItem)
        {
            return _currentLanguage.GetItemName(idItem);
        }

        public static string GetSpellName(int idSpell)
        {
            return _currentLanguage.GetSpellName(idSpell);
        }

        public static string GetSpellDescription(int idSpell)
        {
            return _currentLanguage.GetSpellDescription(idSpell);
        }

        public static string GetErrorText(int idError)
        {
            return _currentLanguage.GetErrorText(idError);
        }

        public static void SetLanguageByName(string name)
        {
            _currentLanguage = LoadLanguageByName(name);
        }

        public static void SetLanguageByPath(string path)
        {
            _currentLanguage = LoadLanguageByPath(path);
        }

        public static Language LoadLanguageByName(string name)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(Settings.pathToLanguagesInResources + name, typeof(TextAsset));
            if (textAsset != null)
            {
                return LoadLanguageByString(textAsset.text);
            }
            else
            {
                return LoadLanguageByPath(Settings.pathToLanguages + name + ".xml");
            }
        }

        public static Language LoadLanguageByPath(string path)
        {
            string text = File.ReadAllText(path);

            return LoadLanguageByString(text);
        }

        private static Language LoadLanguageByString(string text)
        {
            var serializer = new XmlSerializer(typeof(Language));
            var stream = new StringReader(text);

            return (Language)serializer.Deserialize(stream);
        }
    }
}

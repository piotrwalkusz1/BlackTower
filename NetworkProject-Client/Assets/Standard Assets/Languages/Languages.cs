using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
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
                XmlDocument document = new XmlDocument();
                document.LoadXml(textAsset.text);

                return LoadLanguageByXmlDocument(document);
            }
            else
            {
                return LoadLanguageByPath(Settings.pathToLanguages + name + ".xml");
            }
        }

        public static Language LoadLanguageByPath(string path)
        {
            XmlDocument document = new XmlDocument();

            document.Load(path);

            return LoadLanguageByXmlDocument(document);
        }

        private static Language LoadLanguageByXmlDocument(XmlDocument document)
        {
            XmlNodeList expressions = document.GetElementsByTagName("language").Item(0).ChildNodes;

            var dictionary = new Dictionary<string, string>();

            foreach (XmlNode expression in expressions)
            {
                dictionary.Add(expression.Name, expression.InnerText);
            }

            return new Language(dictionary);
        }
    }
}

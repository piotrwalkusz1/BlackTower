using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Standard
{
    public static class Languages
    {
        public const string LANGUAGE_NAME_PATTERN = @"^Language-[^ ]*$";

        private static Language _currentLanguage;
        private static List<Language> _allLanguages;

        public static bool AreLanguagesFromRepositoryLoaded()
        {
            return _allLanguages != null;
        }

        public static Language[] GetAllLanguages()
        {
            return _allLanguages.ToArray();
        }

        public static void SetAllLanguages(Language[] languages)
        {
            _allLanguages.Clear();

            _allLanguages.AddRange(languages);
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

        public static void LoadAndSetAllLanguagesFromResources()
        {
            _allLanguages = LoadAllLanguagesFromResources();
        }

        public static List<Language> LoadAllLanguagesFromResources()
        {
            var allLanguages = new List<Language>();

            TextAsset[] textAssets = Resources.FindObjectsOfTypeAll<TextAsset>();

            foreach (var textAsset in textAssets)
            {
                if (Regex.IsMatch(textAsset.name, LANGUAGE_NAME_PATTERN, RegexOptions.IgnoreCase))
                {
                    allLanguages.Add(LoadLanguageByString(textAsset.text));
                }
                
            }

            return allLanguages;
        }

        public static List<Language> LoadAllLanguagesFromResourcesDirectly()
        {
            var allLanguages = new List<Language>();

            var files = Directory.GetFiles(Settings.pathToLanguagesInUnity);

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                if (Regex.IsMatch(fileName, LANGUAGE_NAME_PATTERN, RegexOptions.IgnoreCase))
                {
                    allLanguages.Add(LoadLanguageFromFile(file));
                }
            }

            return allLanguages;
        }

        public static void SetLanguage(string path, string name)
        {
            _currentLanguage = LoadLanguageFromFileOrResources(path, name);
        }

        public static Language LoadLanguageFromFileOrResources(string pathToFolder, string name)
        {
            try
            {
                return LoadLanguageFromFile(pathToFolder + '/' + name + ".xml");
            }
            catch
            {
                return LoadLanguageFromResources(name);
            }
        }

        public static Language LoadLanguageFromResources(string name)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(name, typeof(TextAsset));

            return LoadLanguageByString(textAsset.text);
        }

        public static Language LoadLanguageFromResourcesDirectly(string name)
        {
            string text = "";

            try
            {
                text = File.ReadAllText(Settings.pathToLanguagesInUnity + name + ".xml");
            }
            catch(FileNotFoundException)
            {

            }

            return LoadLanguageByString(text);
        }

        public static Language LoadLanguageFromFile(string path)
        {
            string text = File.ReadAllText(path);

            return LoadLanguageByString(text);
        }

        public static void SaveAllLanguagesToResources()
        {
            var serializer = new XmlSerializer(typeof(Language)); 

            foreach (var language in _allLanguages)
            {
                using (var stream = new StreamWriter(Settings.pathToLanguagesInUnity + language.Name + ".xml"))
                {
                    serializer.Serialize(stream, language);
                }
            }
        } 

        private static Language LoadLanguageByString(string text)
        {
            var serializer = new XmlSerializer(typeof(Language));
            var stream = new StringReader(text);

            return (Language)serializer.Deserialize(stream);
        }
    }
}

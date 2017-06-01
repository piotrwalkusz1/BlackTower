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
        public const string ITEM_NAME = "itemName";
        public const string SPELL_NAME = "spellName";
        public const string SPELL_DESCRIPTION = "spellDescription";
        public const string MONSTER_NAME = "monsterName";
        public const string QUEST_NAME = "questName";
        public const string QUEST_DESCRIPTION = "questDescription";
        public const string DIALOG = "dialog";
        public const string BUFF_NAME = "buffName";
        public const string BUFF_DESCRIPTION = "buffDescription";
        public const string MESSAGE_TEXT = "messageText";

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

        public static string GetPhrase(string name)
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

        public static string GetMonsterName(int idMonster)
        {
            return _currentLanguage.GetMonsterName(idMonster);
        }

        public static string GetMessageText(int idMessageText)
        {
            return _currentLanguage.GetMessageText(idMessageText);
        }

        public static string GetQuestName(int idQuest)
        {
            return _currentLanguage.GetQuestName(idQuest);
        }

        public static string GetQuestDescription(int idQuest)
        {
            return _currentLanguage.GetQuestDescription(idQuest);
        }

        public static string GetDialog(int idDialog)
        {
            return _currentLanguage.GetDialog(idDialog);
        }

        public static string GetBuffName(int idBuff)
        {
            return _currentLanguage.GetBuffName(idBuff);
        }

        public static string GetBuffDescription(int idBuff)
        {
            return _currentLanguage.GetBuffDescription(idBuff);
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
                    try
                    {
                        allLanguages.Add(LoadLanguageFromFile(file));
                    }
                    catch
                    {
                    }
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
            SaveAllLanguagesToResources(_allLanguages);
        }

        public static void SaveAllLanguagesToResources(List<Language> languages)
        {
            var serializer = new XmlSerializer(typeof(LanguageToSave));         

            foreach (var language in languages)
            {
                using (var stream = new StreamWriter(Settings.pathToLanguagesInUnity + language.Name + ".xml"))
                {
                    serializer.Serialize(stream, new LanguageToSave(language));
                }
            }
        }

        public static LanguagePhrases LoadLanguagePhrases()
        {
            string text = "";

            try
            {
                text = File.ReadAllText(Settings.pathToLanguagePhrases);
            }
            catch (FileNotFoundException)
            {

            }

            try
            {
                var serializer = new XmlSerializer(typeof(LanguagePhrases));
                using (var stream = new StringReader(text))
                {
                    return (LanguagePhrases)serializer.Deserialize(stream);
                }
            }
            catch
            {
                MonoBehaviour.print("Plik ma niewłaściwą budowę. Zostanie zwrócony pusty obiekt.");

                return new LanguagePhrases();
            }          
        }

        public static void SaveLanguagesPhrases(LanguagePhrases phrases)
        {
            var serializer = new XmlSerializer(typeof(LanguagePhrases));

            using (var stream = new FileStream(Settings.pathToLanguagePhrases, FileMode.Create))
            {
                serializer.Serialize(stream, phrases);
            }
        }

        private static Language LoadLanguageByString(string text)
        {
            var serializer = new XmlSerializer(typeof(LanguageToSave));
            var stream = new StringReader(text);

            var language = (LanguageToSave)serializer.Deserialize(stream);

            return language.GetLanguage();
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Xml.Serialization;
using System.IO;

namespace Standard
{
    static public class Settings
    {
        public static readonly string pathToInputsInResources = "defaultInputs";
        public static readonly string pathToInputsInUnity = Application.dataPath + "/Resources/defaultInputs.txt";
        public static readonly string pathToInputsInApplication = Application.dataPath + "/inputs.txt";
        public static readonly string pathToLanguagesInUnity = Application.dataPath + "/Resources/";
        public static readonly string pathToLanguagesInApplication = Application.dataPath + "/Languages/";
        public static readonly string pathToLanguagePhrases = Application.dataPath + "/Editor/Languages/languagePhrases.xml";
        public static readonly string pathToItemsInResources = "items";
        public static readonly string pathToSpellsInResources = "spells";
        public static readonly string pathToBuffsInResources = "buffs";
        public static readonly string pathToConversationsInResources = "conversations";        
        public static readonly string pathToMonstersInResources = "monsters";
        public static readonly string pathToQuestsInResources = "quests";
        
        public static readonly float jumpAndFallSpeed = 2f;
        public static readonly float playerHalfHeight = 0.9f;
        public static readonly float playerDistanceToGround = 0.1f;

        public static Configuration UserConfiguration { get; private set; }

        private static readonly string pathToConfigurationInResources = "defaultSettings";
        private static readonly string pathToConfigurationInUnity = Application.dataPath + "/Resources/defaultSettings.xml";
        private static readonly string pathToConfigurationInApplication = Application.dataPath + "/settings.xml";

        public static void SetSettingsFromFileOrResources()
        {
            try
            {
                SetSettingsFromFile();
            }
            catch
            {
                SetSettingsFromResources();
            }
        }

        public static void SetSettingsFromFile()
        {
            string text = File.ReadAllText(pathToConfigurationInApplication);

            SetConfigurationFromText(text);
        }

        public static void SetSettingsFromResources()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(pathToConfigurationInResources);

            SetConfigurationFromText(textAsset.text);
        }

        public static Configuration LoadConfigurationFromResources()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(pathToConfigurationInResources);

            if (textAsset == null)
            {
                return new Configuration();
            }
            else
            {
                return GetConfigurationFromText(textAsset.text);
            }        
        }

        public static Configuration LoadConfigurationFromResourcesDirectly()
        {
            string text = "";

            try
            {
                text = File.ReadAllText(pathToConfigurationInUnity);
            }
            catch (FileNotFoundException)
            {
                return new Configuration();
            }
            
            return GetConfigurationFromText(text);
        }

        public static void SaveConfigurationToResources()
        {
            SaveConfigurationToResources(UserConfiguration);
        }

        public static void SaveConfigurationToResources(Configuration settings)
        {
            var serializer = new XmlSerializer(typeof(Configuration));

            using (var stream = new StreamWriter(pathToConfigurationInUnity))
            {
                serializer.Serialize(stream, settings);
            }        
        }

        private static void SetConfigurationFromText(string text)
        {
            UserConfiguration = GetConfigurationFromText(text);
        }

        private static Configuration GetConfigurationFromText(string text)
        {
            var serializer = new XmlSerializer(typeof(Configuration));

            var stream = new StringReader(text);

            try
            {
                return (Configuration)serializer.Deserialize(stream);
            }
            catch
            {
                MonoBehaviour.print("Plik zawiera niewłaściwe dane. Zostaną utworzone puste konfiguracje.");

                return new Configuration();
            }
        }
    }
}



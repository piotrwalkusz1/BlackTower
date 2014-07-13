using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Standard;

namespace EditorExtension
{
    public class LanguagesWindow : EditorWindow
    {
        public static LanguagePhrases Phrases { get; set; }
        public static List<LanguageWindow> LanguagesList { get; set; }

        private static bool _isActivePhrasesList;
        private static List<PhrasesCategoryWindow> _phrasesCategories;

        [MenuItem("Extension/Languages")]
        static void ShowWindow()
        {
            LanguagesWindow window = EditorWindow.GetWindow(typeof(LanguagesWindow)) as LanguagesWindow;
            window.Start();
        }

        public void Start()
        {
            Phrases = Languages.LoadLanguagePhrases();
            _phrasesCategories = new List<PhrasesCategoryWindow>();

            foreach (var category in Phrases.GetCategories())
            {
                _phrasesCategories.Add(new PhrasesCategoryWindow(category));
            }

            LanguagesList = new List<LanguageWindow>();

            foreach (var language in Languages.LoadAllLanguagesFromResourcesDirectly())
            {
                LanguagesList.Add(new LanguageWindow(language));
            }
        }        

        private void OnGUI()
        {
            if(_isActivePhrasesList = EditorGUILayout.Foldout(_isActivePhrasesList, "Phrases list"))
            {
                _phrasesCategories.ForEach(x => x.Draw());
            }

            LanguagesList.ForEach(x => x.Draw());

            UpdateLanguages();

            ShowSave();

            ShowContextMenu();
        }

        private void ShowSave()
        {
            if (GUILayout.Button("Save"))
            {
                Save();
            }
        }

        private void Save()
        {
            Languages.SaveLanguagesPhrases(Phrases);

            List<Language> languages = new List<Language>();

            foreach(var language in LanguagesList)
            {
                languages.Add(language.Language);
            }

            Languages.SaveAllLanguagesToResources(languages);
        }

        private void ShowContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add item name"), false, delegate() { Phrases.Phrases.Add(Languages.ITEM_NAME); UpdateLanguages(); });
                menu.AddItem(new GUIContent("Add language"), false, () => LanguagesList.Add(new LanguageWindow(new Language(new Dictionary<string,string>()))));

                menu.ShowAsContext();

                evt.Use();
            }
        }

        private void UpdateLanguages()
        {
            foreach (var language in LanguagesList)
            {
                language.UpdatePhrases();
            }
        }
    }
}

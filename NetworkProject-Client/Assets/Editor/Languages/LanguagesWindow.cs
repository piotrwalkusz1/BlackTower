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
        private static Vector2 _scrollPosition;

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

        public void Save()
        {
            Languages.SaveLanguagesPhrases(Phrases);

            List<Language> languages = new List<Language>();

            foreach (var language in LanguagesList)
            {
                languages.Add(language.Language);
            }

            Languages.SaveAllLanguagesToResources(languages);
        }

        public void AddPhraseAndUpdateLanguages(string phrasePrefix)
        {
            AddPharse(phrasePrefix);

            UpdateLanguages();
        }

        private void OnGUI()
        {
            try
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

                if (_isActivePhrasesList = EditorGUILayout.Foldout(_isActivePhrasesList, "Phrases list"))
                {
                    _phrasesCategories.ForEach(x => x.Draw());
                }

                LanguagesList.ForEach(x => x.Draw());

                UpdateLanguages();

                ShowSave();

                ShowContextMenu();

                GUILayout.EndScrollView();
            }
            catch (NullReferenceException ex)
            {
                Start();

                MonoBehaviour.print(ex.ToString() + '\n' + ex.StackTrace);
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.ToString() + '\n' + ex.StackTrace);
            }
        }

        private void ShowSave()
        {
            if (GUILayout.Button("Save"))
            {
                Save();
            }
        }

        

        private void ShowContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add phrase"), false, delegate() { AddPhraseAndUpdateLanguages(""); });
                menu.AddItem(new GUIContent("Add item name"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.ITEM_NAME); });
                menu.AddItem(new GUIContent("Add spell name"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.SPELL_NAME); });
                menu.AddItem(new GUIContent("Add spell description"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.SPELL_DESCRIPTION); });
                menu.AddItem(new GUIContent("Add monster name"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.MONSTER_NAME); });
                menu.AddItem(new GUIContent("Add quest name"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.QUEST_NAME); });
                menu.AddItem(new GUIContent("Add quest description"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.QUEST_DESCRIPTION); });
                menu.AddItem(new GUIContent("Add dialog"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.DIALOG); });
                menu.AddItem(new GUIContent("Add message text"), false, delegate() { AddPhraseAndUpdateLanguages(Languages.MESSAGE_TEXT); });
                menu.AddItem(new GUIContent("Add language"), false, () => LanguagesList.Add(new LanguageWindow(new Language())));

                menu.ShowAsContext();

                evt.Use();
            }
        }

        private void AddPharse(string prefix)
        {
            for (int i = 0; ; i++)
            {
                string newPhrase = prefix + i.ToString();

                if(!Phrases.IsContain(newPhrase))
                {
                    Phrases.Add(newPhrase);
                    break;
                }
            }
        }

        private void UpdateLanguages()
        {
            foreach (var language in LanguagesList)
            {
                language.Language.UpdatePhrases(Phrases.AllPhrases.ToArray());
            }
        }
    }
}

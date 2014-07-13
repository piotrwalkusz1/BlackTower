using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class LanguageWindow
    {
        public Language Language;

        private bool _isActive;

        public LanguageWindow(Language language)
        {
            Language = language;

            UpdatePhrases();
        }

        public void Draw()
        {
            if (_isActive = EditorGUILayout.Foldout(_isActive, Language.Name))
            {
                Language.Name = EditorGUILayout.TextField("Language name", Language.Name);

                var phrases = Language.Pharses;
                var keys = phrases.Keys.ToList();

                foreach (var key in keys)
                {
                    phrases[key] = EditorGUILayout.TextField(key, phrases[key]);
                }
            }
        }

        public void UpdatePhrases()
        {
            var phrasesAlrightContained = new List<string>();

            foreach (var phrase in LanguagesWindow.Phrases.Phrases)
            {
                if (Language.ContainPhrase(phrase))
                {
                    phrasesAlrightContained.Add(phrase);
                }
                else
                {
                    Language.SetPhrase(phrase, "");
                }
            }

            foreach()
            {

            }
        }
    }
}

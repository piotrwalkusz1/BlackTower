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

            Language.UpdatePhrases(LanguagesWindow.Phrases.AllPhrases.ToArray());
        }

        public void Draw()
        {
            if (_isActive = EditorGUILayout.Foldout(_isActive, Language.Name))
            {
                Indentation.BeginIndentation();

                Language.Name = EditorGUILayout.TextField("Language name", Language.Name);

                var phrases = Language.Pharses;
                var keys = phrases.Keys.OrderBy(x => x).ToArray();

                foreach (var key in keys)
                {
                    phrases[key] = EditorGUILayout.TextField(key, phrases[key]);
                }

                Indentation.EndIndentation();
            }
        }
    }
}

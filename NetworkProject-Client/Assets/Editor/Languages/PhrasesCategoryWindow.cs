using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Standard;

namespace EditorExtension
{
    public class PhrasesCategoryWindow
    {
        public PhrasesCategory PharsesCategory { get; set; }

        private bool _isActive;

        public PhrasesCategoryWindow(PhrasesCategory pharsesCategory)
        {
            PharsesCategory = pharsesCategory;
        }

        public void Draw()
        {
            if (_isActive = EditorGUILayout.Foldout(_isActive, PharsesCategory.Name))
            {
                Indentation.BeginIndentation();

                Indentation.BeginIndentation();

                foreach (var pharse in PharsesCategory.GetPharses())
                {
                    PharsesCategory.Change(pharse, EditorGUILayout.TextField(pharse));
                }

                Indentation.EndIndentation();

                Indentation.EndIndentation();
            }
        }
    }
}

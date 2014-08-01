using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class SpellWindow
    {
        public VisualSpellData Spell;

        private bool _isActiveRequirements;
        private int _addRequirementSelectedIndex;

        private bool _isActive;

        public SpellWindow()
        {
            Spell = new VisualSpellData(0);
        }

        public SpellWindow(VisualSpellData spell)
        {
            Spell = spell;
        }

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal();

            _isActive = EditorGUILayout.Foldout(_isActive, "Spell " + Spell.IdSpell.ToString());

            ShowDeleteSpell();

            EditorGUILayout.EndHorizontal();

            if(_isActive)
            {
                Indentation.BeginIndentation();

                Spell.IdSpell = EditorGUILayout.IntField("Id spell", Spell.IdSpell);
                Spell.IdImage = EditorGUILayout.IntField("Id image", Spell.IdImage);
                Spell.Cooldown = EditorGUILayout.FloatField("Cooldown", Spell.Cooldown);

                ShowRequirements();
                
                Indentation.EndIndentation();
            }
        }

        public void ShowRequirements()
        {
            if (_isActiveRequirements = EditorGUILayout.Foldout(_isActiveRequirements, "Requirements"))
            {
                Indentation.BeginIndentation();

                foreach (var requirement in Spell.GetRequirements())
                {
                    EditorGUILayout.BeginHorizontal();

                    try
                    {
                        EditorGUILayout.LabelField(requirement.GetType().Name);
                        requirement.Set(EditorGUILayout.TextField(requirement.GetAsString()));
                    }
                    catch { }

                    EditorGUILayout.EndHorizontal();
                }


                EditorGUILayout.BeginHorizontal();

                ShowAddRequirement();

                _addRequirementSelectedIndex = EditorGUILayout.Popup(_addRequirementSelectedIndex,
                    SpellsWindow.RequirementsList.GetTypesNames());

                EditorGUILayout.EndHorizontal();

                Indentation.EndIndentation();
            }
        }

        private void ShowAddRequirement()
        {
            if (GUILayout.Button("Add new requirement"))
            {
                AddRequiremnt();
            }
        }

        private void AddRequiremnt()
        {
            Spell.AddRequirement(SpellsWindow.RequirementsList.CreateInstantiateByIndex(_addRequirementSelectedIndex));
        }

        private void ShowDeleteSpell()
        {
            if (GUILayout.Button("Delete : " + Spell.IdSpell))
            {
                DeleteSpell();
            }
        }

        private void DeleteSpell()
        {
            SpellsWindow.DeleteSpell(this);
        }
    }
}

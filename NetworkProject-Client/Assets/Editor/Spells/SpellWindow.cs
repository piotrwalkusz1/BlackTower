using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject.Spells;

namespace EditorExtension
{
    public class SpellWindow
    {
        public SpellData Spell;

        private bool _isActiveRequirements;
        private bool _isActiveRequiredInfo;
        private int _addRequirementSelectedIndex;

        private bool _isActive;

        public SpellWindow()
        {
            Spell = new SpellData();
        }

        public SpellWindow(SpellData spell)
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
                Spell.ManaCost = EditorGUILayout.IntField("Mana cost", Spell.ManaCost);
                Spell.Cooldown = EditorGUILayout.FloatField("Cooldown", Spell.Cooldown);

                ShowRequirements();

                ShowRequiredInfo();
                
                Indentation.EndIndentation();
            }
        }

        public void ShowRequirements()
        {
            if (_isActiveRequirements = EditorGUILayout.Foldout(_isActiveRequirements, "Requirements"))
            {
                Indentation.BeginIndentation();

                foreach (var requirement in Spell.Requirements)
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

        public void ShowRequiredInfo()
        {
            if(_isActiveRequiredInfo = EditorGUILayout.Foldout(_isActiveRequiredInfo, "Required info"))
            {
                if (Spell.RequiredInfo == null)
                {
                    Spell.RequiredInfo = new SpellRequiredInfo();
                }
                Indentation.BeginIndentation();

                Spell.RequiredInfo._targetObject = EditorGUILayout.Toggle("Target object", Spell.RequiredInfo._targetObject);
                Spell.RequiredInfo._targetPosition = EditorGUILayout.Toggle("Target position", Spell.RequiredInfo._targetPosition);

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
            Spell.Requirements.Add(SpellsWindow.RequirementsList.CreateInstantiateByIndex(_addRequirementSelectedIndex));
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

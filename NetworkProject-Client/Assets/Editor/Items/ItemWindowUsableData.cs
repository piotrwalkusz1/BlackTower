using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class ItemWindowUsableData : ItemWindowData
    {
        public override ItemData Item
        {
            get { return ItemUsable; }
            protected set { ItemUsable = (ItemUsableData)value; }
        }
        public virtual ItemUsableData ItemUsable { get; protected set; }

        private bool _isActiveActions;
        private int _addActionSelectedIndex;

        public ItemWindowUsableData(ItemUsableData item)
            : base(item)
        {

        }

        public override void Draw()
        {
            base.Draw();

            ShowActions();
        }

        private void ShowActions()
        {
            if (_isActiveActions = EditorGUILayout.Foldout(_isActiveActions, "Actions"))
            {
                Indentation.BeginIndentation();

                foreach (var action in ItemUsable.Actions)
                {
                    EditorGUILayout.BeginHorizontal();

                    try
                    {
                        EditorGUILayout.LabelField(action.GetType().Name);
                        action.SetValue(EditorGUILayout.TextField(action.GetValueAsText()));
                    }
                    catch { }

                    EditorGUILayout.EndHorizontal();
                }


                EditorGUILayout.BeginHorizontal();

                ShowAddAction();

                _addActionSelectedIndex = EditorGUILayout.Popup(_addActionSelectedIndex,
                    ItemsWindow.UseActionsList.GetTypesNames());

                EditorGUILayout.EndHorizontal();

                Indentation.EndIndentation();
            }
        }

        private void ShowAddAction()
        {
            if (GUILayout.Button("Add new action"))
            {
                AddAction();
            }
        }

        private void AddAction()
        {
            ItemUsable.Actions.Add(ItemsWindow.UseActionsList.CreateInstantiateByIndex(_addActionSelectedIndex));
        }

        public override string GetItemTypeName()
        {
            return "Usable";
        }
    }
}

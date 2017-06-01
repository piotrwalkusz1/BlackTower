using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject.Items;

namespace EditorExtension
{
    public class ItemWindow
    {
        public ItemWindowData ItemWindowData { get; set; }

        public ItemData ItemData
        {
            get { return ItemWindowData.Item; }
        }

        private bool _isShowed = false;      

        public ItemWindow(ItemWindowData item)
        {
            ItemWindowData = item;
        }

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal();

            _isShowed = EditorGUILayout.Foldout(_isShowed, ItemWindowData.IdItem.ToString() + ' ' + ItemWindowData.GetItemTypeName());
            ShowDelete();

            EditorGUILayout.EndHorizontal();

            if(_isShowed)
            {
                Indentation.BeginIndentation();
                ItemWindowData.Draw();
                Indentation.EndIndentation();
            }
        }

        private void ShowDelete()
        {
            if (GUILayout.Button("Delete : " + ItemData.IdItem, GUILayout.MaxWidth(100f)))
            {
                Delete();
            }
        }

        private void Delete()
        {
            ItemsWindow.Delete(this);
        }
    }
}

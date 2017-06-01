using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject;
using NetworkProject.Benefits;

namespace EditorExtension
{
    public class BuffsWindow : EditorWindow
    {
        public static List<BuffWindow> _windows;

        public static TypeFinder<IBenefit> _benefitsList;

        [MenuItem("Extension/Buffs")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(BuffsWindow)) as BuffsWindow;
            window.Start();
        }

        public void Start()
        {
            _benefitsList = new TypeFinder<IBenefit>();

            _windows = new List<BuffWindow>();

            List<BuffData> buffs = EditorSaveLoad.LoadBuffs();

            foreach (var buff in buffs)
            {
                _windows.Add(new BuffWindow(buff));
            }
        }

        private void OnGUI()
        {
            foreach (var window in _windows)
            {
                window.Draw();
            }

            ShowAddBuff();

            ShowSave();
        }

        private void ShowAddBuff()
        {
            if (GUILayout.Button("Add new buff"))
            {
                AddBuff();
            }
        }

        private void AddBuff()
        {
            _windows.Add(new BuffWindow());
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
            var buffs = new List<BuffData>();

            foreach (var window in _windows)
            {
                buffs.Add(window.Buff);
            }

            EditorSaveLoad.SaveBuffs(buffs);
        }
    }
}

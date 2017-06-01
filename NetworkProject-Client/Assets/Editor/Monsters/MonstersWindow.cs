using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject;
using NetworkProject.Monsters;

namespace EditorExtension
{
    public class MonstersWindow : EditorWindow
    {
        private List<MonsterWindow> _monsters;

        [MenuItem("Extension/Monsters")]
        static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(MonstersWindow)) as MonstersWindow;
            window.Start();
        }

        public void Start()
        {
            _monsters = new List<MonsterWindow>();

            foreach (var monster in EditorSaveLoad.LoadMonsters())
            {
                _monsters.Add(new MonsterWindow(monster));
            }
        }

        private void OnGUI()
        {
            foreach (var monster in _monsters)
            {
                monster.Draw();
            }

            ShowContextMenu();

            ShowSave();
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
            var monsters = new List<MonsterMultiData>();

            _monsters.ForEach(x => monsters.Add(x.Monster));

            EditorSaveLoad.SaveMonsters(monsters);
        }

        private void ShowContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add monster"), false, AddMonster);

                menu.ShowAsContext();

                evt.Use();
            }
        }

        private void AddMonster()
        {
            _monsters.Add(new MonsterWindow(new MonsterMultiData()));
        }
    }
}

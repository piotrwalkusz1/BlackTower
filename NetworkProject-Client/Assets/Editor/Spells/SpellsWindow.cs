using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using NetworkProject;
using NetworkProject.Requirements;

namespace EditorExtension
{
    public class SpellsWindow : EditorWindow
    {
        public static TypeFinder<ISpellCasterRequirement> RequirementsList = new TypeFinder<ISpellCasterRequirement>();

        private List<SpellWindow> _spells = new List<SpellWindow>();
        private static List<SpellWindow> _spellsToDelete = new List<SpellWindow>();

        [MenuItem("Extension/Spells")]
        private static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(SpellsWindow)) as SpellsWindow;
            window.Start();
        }

        public void Start()
        {
            foreach (var spell in EditorSaveLoad.LoadSpells())
            {
                _spells.Add(new SpellWindow(spell));
            }
        }

        private void OnGUI()
        {
            _spells.ForEach(x => x.Draw());

            ShowContextMenu();

            ShowSave();

            RemoveSpellsToDelete();
        }

        public static void DeleteSpell(SpellWindow spell)
        {
            _spellsToDelete.Add(spell);
        }

        private void RemoveSpellsToDelete()
        {
            foreach (var spell in _spellsToDelete)
	        {
                _spells.Remove(spell);
	        }

            _spellsToDelete.Clear();
        }

        private void ShowContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add spell"), false, AddSpell);

                menu.ShowAsContext();

                evt.Use();
            }
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
            var spells = new List<VisualSpellData>();

            _spells.ForEach(x => spells.Add(x.Spell));

            EditorSaveLoad.SaveSpells(spells);
        }

        private void AddSpell()
        {
            _spells.Add(new SpellWindow());
        }
    }
}

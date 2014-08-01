using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Standard;

namespace EditorExtension
{
    public class SettingsWindow : EditorWindow
    {
        public static Configuration EditingConfiguration { get; set; }

        private bool _isLanguagesNameActive;

        public const float INDENTATION = 15f;

        [MenuItem("Extension/Settings")]
        static void ShowWindow()
        {
            SettingsWindow window = EditorWindow.GetWindow(typeof(SettingsWindow), false, "Settings") as SettingsWindow;

            window.Start();
        }

        public void Start()
        {
            EditingConfiguration = Settings.LoadConfigurationFromResourcesDirectly();
        }

        void OnGUI()
        {
            EditingConfiguration.DefaultLanguageName = EditorGUILayout.TextField("Default language", EditingConfiguration.DefaultLanguageName);

            ShowSaveSettings();
        }

        private void ShowSaveSettings()
        {
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            Settings.SaveConfigurationToResources(EditingConfiguration);
        }
    }
}

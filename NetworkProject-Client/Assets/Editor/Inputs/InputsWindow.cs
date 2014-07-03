using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using ExtendedClassesInputsEditor;
using InputsSystem;
using SaveLoadSystem;
using System.Linq;
using System.IO;
using Standard;

namespace EditorExtension
{
    public class InputsWindow : EditorWindow
    {
        static private readonly string pathToDeleteImage = "file://" + Application.dataPath + "/Editor/Inputs/GUI/delete.png";
        static public Texture2D _deleteImage;

        public const float _indentation = 15f;

        private bool _isButtonsShowed = true;
        private bool _isAxesShowed = true;
        private Vector2 scrollPosition = new Vector2();

        private List<ButtonInputsWindow> _buttons = new List<ButtonInputsWindow>();
        private List<AxisInputsWindow> _axes = new List<AxisInputsWindow>();

        [MenuItem("Extending/Inputs")]
        static void ShowWindow()
        {
            InputsWindow inputsWindow = EditorWindow.GetWindow(typeof(InputsWindow), false, "Inputs") as InputsWindow;
            inputsWindow.Start();
        }

        static private void LoadButtonDelete()
        {
            WWW www = new WWW(pathToDeleteImage);
            _deleteImage = www.texture;
        }

        public void Start()
        {
            LoadButtonDelete();
            LoadInputs();
        }

        private void LoadInputs()
        {
            InputsToSave inputs;
            try
            {
                inputs = SaveLoad.Load<InputsToSave>(Settings.pathToDefaultInputs);
            }
            catch(FileNotFoundException)
            {
                inputs = new InputsToSave();
            }

            _buttons = inputs.Buttons.ToList();
            _axes = inputs.Axes.ToList();
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            DrawButtonsSection();
            DrawAxesSection();
            ShowSaveInputs();
            GUILayout.EndScrollView();
        }

        private void DrawButtonsSection()
        {
            _isButtonsShowed = EditorGUILayout.Foldout(_isButtonsShowed, "Buttons");
            if (_isButtonsShowed)
            {
                Indentation.BeginIndentation(_indentation);
                DrawButtons();
                Indentation.EndIndentation();
            }
            ShowAddButton();
        }

        private void DrawButtons()
        {
            ButtonInputsWindow[] buttons = _buttons.ToArray();
            foreach (ButtonInputsWindow button in buttons)
            {
                GUILayout.BeginHorizontal();
                button.Draw();
                ShowDeleteButton(button);
                GUILayout.EndHorizontal();
            }
        }

        private void ShowDeleteButton(ButtonInputsWindow button)
        {
            if (GUILayout.Button(_deleteImage, GUILayout.Width(20f)))
            {
                DeleteButton(button);
            }
        }

        private void DeleteButton(ButtonInputsWindow button)
        {
            _buttons.Remove(button);
        }

        private void ShowAddButton()
        {
            BeginWindows();
            if (GUILayout.Button("Add button"))
            {
                CreateNewButton();
            }
            EndWindows();
        }

        private void CreateNewButton()
        {
            string name = ChooseButtonName();
            AddButton(name);
        }

        private string ChooseButtonName()
        {
            const string basis = "new button";
            string newName;
            for (int i = 0; true; i++)
            {
                newName = basis + i.ToString();
                if (!ExistButtonNamed(newName))
                {
                    return newName;
                }
            }
        }

        private bool ExistButtonNamed(string name)
        {
            return _buttons.ExistButtonNamed(name);
        }

        private void AddButton(string name)
        {
            _buttons.Add(new ButtonInputsWindow(name, new InputButtonDown()));
        } 

        private void DrawAxesSection()
        {
            _isAxesShowed = EditorGUILayout.Foldout(_isAxesShowed, "Axes");
            if (_isAxesShowed)
            {
                Indentation.BeginIndentation(_indentation);
                DrawAxes();
                Indentation.EndIndentation();
            }
            ShowAddAxis();
        }

        private void DrawAxes()
        {
            AxisInputsWindow[] axes = _axes.ToArray();
            foreach (AxisInputsWindow axis in axes)
            {
                GUILayout.BeginHorizontal();
                axis.Draw();
                ShowDeleteAxis(axis);
                GUILayout.EndHorizontal();
            }
        }

        private void ShowDeleteAxis(AxisInputsWindow axis)
        {
            if (GUILayout.Button(_deleteImage, GUILayout.Width(20f)))
            {
                DeleteAxis(axis);
            }
        }

        private void DeleteAxis(AxisInputsWindow axis)
        {
            _axes.Remove(axis);
        }

        private void ShowAddAxis()
        {
            BeginWindows();
            if (GUILayout.Button("Add axis"))
            {
                CreateNewAxis();
            }
            EndWindows();
        }

        private void CreateNewAxis()
        {
            string name = ChooseAxisName();
            AddAxis(name);
        }

        private string ChooseAxisName()
        {
            const string basis = "new axis";
            string newName;
            for (int i = 0; true; i++)
            {
                newName = basis + i.ToString();
                if (!ExistAxisNamed(newName))
                {
                    return newName;
                }
            }
        }

        private bool ExistAxisNamed(string name)
        {
            return _axes.ExistAxisNamed(name);
        }

        private void AddAxis(string name)
        {
            _axes.Add(new AxisInputsWindow(name, new InputAxis()));
        }

        private void ShowSaveInputs()
        {
            BeginWindows();
            if (GUILayout.Button("Save"))
            {
                SaveInputs();
            }
            EndWindows();
        }

        private void SaveInputs()
        {
            InputsToSave inputsToSave = new InputsToSave();
            inputsToSave.Buttons = _buttons.ToDictionary();
            inputsToSave.Axes = _axes.ToDictionary();
            SaveLoad.Save(inputsToSave, Settings.pathToDefaultInputs);
        }
    }
}

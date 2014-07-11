using UnityEngine;
using UnityEditor;
using System.Collections;
using ExtendedClassesInputsEditor;
using InputsSystem;
using SaveLoadSystem;

namespace EditorExtension
{
    public class ButtonInputsWindow
    {
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public InputButtonDown Button
        {
            get
            {
                return _button;
            }
            set
            {
                _button = value;
            }
        }
        private bool _isShowed = false;
        private string _name;
        private InputButtonDown _button;

        public ButtonInputsWindow(string name, InputButtonDown button)
        {
            _name = name;
            _button = button;
        }

        public void Show()
        {
            _isShowed = true;
        }

        public void Hide()
        {
            _isShowed = false;
        }

        public void Draw()
        {
            GUILayout.BeginVertical();
            _isShowed = EditorGUILayout.Foldout(_isShowed, _name);
            if (_isShowed)
            {
                Indentation.BeginIndentation(InputsWindow.INDENTATION);
                _name = EditorGUILayout.TextField("Name :", _name);
                _button.DrawFieldsAdjustment();
                Indentation.EndIndentation();
            }
            GUILayout.EndVertical();
        }
    }
}



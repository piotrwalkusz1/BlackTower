using UnityEngine;
using UnityEditor;
using System.Collections;
using ExtendedClassesInputsEditor;
using InputsSystem;

namespace EditorExtension
{
    public class AxisInputsWindow
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
        public InputAxis Axis
        {
            get
            {
                return _axis;
            }
            set
            {
                _axis = value;
            }
        }
        private bool _isShowed;
        private string _name;
        private InputAxis _axis;

        public AxisInputsWindow(string name, InputAxis axis)
        {
            _name = name;
            _axis = axis;
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
            _isShowed = EditorGUILayout.Foldout(_isShowed, _name);
            if (_isShowed)
            {
                Indentation.BeginIndentation(InputsWindow._indentation);
                _name = EditorGUILayout.TextField("Name :", _name);
                _axis.DrawFieldsAdjustment();
                Indentation.EndIndentation();
            }
        }
    }
}


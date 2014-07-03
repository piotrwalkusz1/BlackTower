using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace InputsSystem
{
    public class InputsData
    {
        public Dictionary<string, InputButtonDown> Buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                _buttons = value;
            }
        }
        public Dictionary<string, InputAxis> Axes
        {
            get
            {
                return _inputAxes;
            }
            set
            {
                _inputAxes = value;
            }
        }

        private Dictionary<string, InputButtonDown> _buttons = new Dictionary<string, InputButtonDown>();
        private Dictionary<string, InputAxis> _inputAxes = new Dictionary<string, InputAxis>();

        public bool DownButton(string name)
        {
            InputButtonDown button = GetButton(name);
            if (button != null)
            {
                return button.DownButton();
            }
            else
            {
                throw new System.ArgumentException();
            }
        }

        public bool UpButton(string name)
        {
            InputButtonDown button = GetButton(name);
            if (button != null)
            {
                return button.UpButton();
            }
            else
            {
                throw new System.ArgumentException();
            }
        }

        public bool IsPressedButton(string name)
        {
            InputButtonDown button = GetButton(name);
            if (button != null)
            {
                return button.IsPressed();
            }
            return false;
        }

        public float GetAxis(string name)
        {
            InputAxis axis;
            Axes.TryGetValue(name, out axis);
            return axis.GetAxisValue();
        }

        public InputButtonDown GetButton(string name)
        {
            InputButtonDown button;
            bool existButton = _buttons.TryGetValue(name, out button);

            if (existButton)
            {
                return button;
            }
            else
            {
                return null;
            }
        }

        public bool AddButton(string name, InputButtonDown inputButton)
        {
            try
            {
                _buttons.Add(name, inputButton);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool ChangeButton(string name, InputButtonDown inputButton)
        {
            try
            {
                _buttons[name] = inputButton;
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void AddOrChangeButton(string name, InputButtonDown inputButton)
        {
            _buttons.Remove(name);
            _buttons.Add(name, inputButton);
        }

        public void AddOrChangeButtons(string[] names, InputButtonDown[] inputButtons)
        {
            if (names.Length != inputButtons.Length)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < inputButtons.Length; i++)
            {
                try
                {
                    AddOrChangeButton(names[i], inputButtons[i]);
                }
                catch (ArgumentException) { }
            }
        }

        public bool AddInputAxis(string name, InputAxis axis)
        {
            try
            {
                _inputAxes.Add(name, axis);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool ChangeInputAxis(string name, InputAxis axis)
        {
            try
            {
                _inputAxes[name] = axis;
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void AddOrChangeInputAxis(string name, InputAxis axis)
        {
            _inputAxes.Remove(name);
            _inputAxes.Add(name, axis);
        }

        public void AddOrChangeInputAxes(string[] names, InputAxis[] axes)
        {
            if (names.Length != axes.Length)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < axes.Length; i++)
            {
                try
                {
                    AddOrChangeInputAxis(names[i], axes[i]);
                }
                catch (ArgumentException) { }
            }
        }

        public void RemoveAll()
        {
            RemoveAllButtons();
            RemoveAllAxes();
        }

        public void RemoveAllButtons()
        {
            _buttons.Clear();
        }

        public void RemoveAllAxes()
        {
            _inputAxes.Clear();
        }
    }
}


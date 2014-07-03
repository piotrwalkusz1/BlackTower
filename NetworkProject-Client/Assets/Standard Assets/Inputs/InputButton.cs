using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Standard;

namespace InputsSystem
{
    public class InputButton
    {
        public KeyCode Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        public KeyCode AltKey
        {
            get
            {
                return _altKey;
            }
            set
            {
                _altKey = value;
            }
        }
        public string TextNumberKey
        {
            get
            {
                return ((int)_key).ToString();
            }
        }
        public string TextNumberAltKey
        {
            get
            {
                return ((int)_altKey).ToString();
            }
        }

        protected bool _isPressed;
        protected KeyCode _key;
        protected KeyCode _altKey;

        public InputButton() : this(KeyCode.A, KeyCode.A) {}

        public InputButton(string key, string altKey)
        {
            _key = (KeyCode)int.Parse(key);
            _altKey = (KeyCode)int.Parse(altKey);
            _isPressed = false;

            Updating.OnGUIEvent += OnGUI;
        }

        public InputButton(KeyCode key, KeyCode altKey)
        {
            _key = key;
            _altKey = altKey;
            _isPressed = false;

            Updating.OnGUIEvent += OnGUI;
        }    

        public virtual bool IsUseThisKey(KeyCode key)
        {
            if (key == _key || key == _altKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool IsPressed()
        {
            return _isPressed;
        }

        protected void OnGUI()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                KeyCode keyCode = Event.current.keyCode;
                if (IsUseThisKey(keyCode))
                {
                    _isPressed = true;
                }
            }
            if (Event.current.type == EventType.KeyUp)
            {
                KeyCode keyCode = Event.current.keyCode;
                if (IsUseThisKey(keyCode))
                {
                    _isPressed = false;
                }
            }
            if (Event.current.type == EventType.MouseDown)
            {
                KeyCode keyCode = MouseButtonToKeyCode(Event.current.button);
                if (IsUseThisKey(keyCode))
                {
                    _isPressed = true;
                }
            }
            if (Event.current.type == EventType.MouseUp)
            {
                KeyCode keyCode = MouseButtonToKeyCode(Event.current.button);
                if (IsUseThisKey(keyCode))
                {
                    _isPressed = false;
                }
            }
        }

        private KeyCode MouseButtonToKeyCode(int button)
        {
            switch (button)
            {
                case 0:
                    return KeyCode.Mouse0;
                case 1:
                    return KeyCode.Mouse1;
                case 2:
                    return KeyCode.Mouse2;
                default:
                    throw new System.Exception();
            }
        }
    }
}


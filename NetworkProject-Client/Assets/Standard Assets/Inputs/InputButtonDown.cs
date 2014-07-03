using UnityEngine;
using System.Collections;
using Standard;

namespace InputsSystem
{
    public class InputButtonDown : InputButton
    {
        protected bool _pressedInLastFrame;

        public InputButtonDown() : base()
        {
            _pressedInLastFrame = false;
        }

        public InputButtonDown(KeyCode key, KeyCode altKey) : base(key, altKey)
        {
            _pressedInLastFrame = false;
            Updating.LateUpdateEvent += LateUpdate;
        }

        public InputButtonDown(string key, string altKey) : base(key, altKey)
        {
            _pressedInLastFrame = false;
            Updating.LateUpdateEvent += LateUpdate;
        }

        public bool DownButton()
        {
            return (_pressedInLastFrame == false && _isPressed == true);
        }

        public bool UpButton()
        {
            return (_pressedInLastFrame == true && _isPressed == false);
        }

        protected void LateUpdate()
        {
            _pressedInLastFrame = _isPressed;
        }
    }
}


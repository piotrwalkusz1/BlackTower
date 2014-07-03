using UnityEngine;
using System.Collections;
using Standard;

namespace InputsSystem
{
    public class InputAxis
    {
        private float _value;

        private InputButton _positiveButton;
        private InputButton _negativeButton;
        private float _sensitivity;
        private float _gravity;

        public bool IsChangedAxisValue { get; private set; }

        public InputButton PositiveButton
        {
            get
            {
                return _positiveButton;
            }
            set
            {
                _positiveButton = value;
            }
        }
        public InputButton NegativeButton
        {
            get
            {
                return _negativeButton;
            }
            set
            {
                _negativeButton = value;
            }
        }
        public float Sensitivity
        {
            get
            {
                return _sensitivity;
            }
            set
            {
                _sensitivity = value;
            }
        }
        public float Gravity
        {
            get
            {
                return _gravity;
            }
            set
            {
                _gravity = value;
            }
        }

        public InputAxis() : this(new InputButton(), new InputButton(), 0f, 0f) {}

        public InputAxis(InputButton positiveButton, InputButton negativeButton, float sensitivity, float gravity)
        {
            _positiveButton = positiveButton;
            _negativeButton = negativeButton;
            _sensitivity = sensitivity;
            _gravity = gravity;

            Updating.UpdateEvent += Update;
        }

        public float GetAxisValue()
        {
            return _value;
        }

        public bool IsZero()
        {
            if (GetAxisValue() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUseThisKey(KeyCode key)
        {
            return IsThisKeyPositive(key) || IsThisKeyNegative(key);
        }

        public bool IsThisKeyPositive(KeyCode key)
        {
            return _positiveButton.IsUseThisKey(key);
        }

        public bool IsThisKeyNegative(KeyCode key)
        {
            return _negativeButton.IsUseThisKey(key);
        }     

        public void IncreaseValue()
        {
            IsChangedAxisValue = true;

            _value += _sensitivity * Time.deltaTime;
            if (_value > 1f)
            {
                _value = 1f;
            }
        }

        public void DecreaseValue()
        {
            IsChangedAxisValue = true;

            _value -= _sensitivity * Time.deltaTime;
            if (_value < -1f)
            {
                _value = -1f;
            }
        }

        public void Update()
        {
            if (_positiveButton.IsPressed())
            {
                IncreaseValue();
            }
            if (_negativeButton.IsPressed())
            {
                DecreaseValue();
            }

            if (!IsChangedAxisValue)
            {
                GraviteAxis();
            }
            IsChangedAxisValue = false;
        }

        private void GraviteAxis()
        {
            _value = Mathf.MoveTowards(_value, 0, _gravity * Time.deltaTime);
        }
    }
}


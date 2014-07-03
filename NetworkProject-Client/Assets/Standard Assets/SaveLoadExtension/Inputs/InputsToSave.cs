using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InputsSystem;

namespace SaveLoadSystem
{
    public class InputsToSave : ISavable
    {
        public Dictionary<string, InputButtonDown> Buttons
        {
            get
            {
                return _inputsData.Buttons;
            }
            set
            {
                _inputsData.Buttons = value;
            }
        }
        public Dictionary<string, InputAxis> Axes
        {
            get
            {
                return _inputsData.Axes;
            }
            set
            {
                _inputsData.Axes = value;
            }
        }
        public InputsData InputsData
        {
            get
            {
                return _inputsData;
            }
            set
            {
                _inputsData = value;
            }
        }

        private InputsData _inputsData;

        private const char _blocksSeparator = '&';
        private const char _linesSeparator = '\n';
        private const char _propertiesSeparator = ';';

        public InputsToSave()
        {
            _inputsData = new InputsData();
        }

        public InputsToSave(InputsData inputsData)
        {
            _inputsData = inputsData;
        }

        public string ToSavableText()
        {
            string buttons = ConvertToBlock(Buttons);
            string axes = ConvertToBlock(Axes);
            string content = String.Join(_blocksSeparator.ToString(), new string[] { buttons, axes });
            return content;
        }

        public void SetByLoadedText(string loadedText)
        {
            try
            {
                string[] blocks = SeparateIntoBlocks(loadedText);
                Buttons = ConvertToButtons(blocks[0]);
                Axes = ConvertToAxes(blocks[1]);
            }
            catch (IndexOutOfRangeException)
            {
                Buttons = new Dictionary<string, InputButtonDown>();
                Axes = new Dictionary<string, InputAxis>();
            }
        }

        private string ConvertToBlock(Dictionary<string, InputButtonDown> buttons)
        {
            List<string> buttonsText = new List<string>();
            string buttonText = null;
            foreach (KeyValuePair<string, InputButtonDown> button in buttons)
            {
                buttonText = ConvertToLine(button);
                buttonsText.Add(buttonText);
            }
            return String.Join(_linesSeparator.ToString(), buttonsText.ToArray());
        }

        private string ConvertToBlock(Dictionary<string, InputAxis> axes)
        {
            List<string> axesText = new List<string>();
            string axisText = null;
            foreach (KeyValuePair<string, InputAxis> axis in axes)
            {
                axisText = ConvertToLine(axis);
                axesText.Add(axisText);
            }
            return String.Join(_linesSeparator.ToString(), axesText.ToArray());
        }

        private string ConvertToLine(KeyValuePair<string, InputAxis> axis)
        {
            string axisName = axis.Key;
            string axisText = ConvertToLine(axis.Value);
            return String.Join(_propertiesSeparator.ToString(), new string[] {axisName, axisText});
        }

        private string ConvertToLine(InputAxis axis)
        {
            string positiveButton = ConvertToLine(axis.PositiveButton);
            string negativeButton = ConvertToLine(axis.NegativeButton);
            string sensitivity = axis.Sensitivity.ToString();
            string gravity = axis.Gravity.ToString();
            return String.Join(_propertiesSeparator.ToString(), new string[] {positiveButton, negativeButton, sensitivity, gravity});
        }

        private string ConvertToLine(KeyValuePair<string, InputButtonDown> button)
        {
            string buttonName = button.Key;
            string buttonText = ConvertToLine(button.Value);
            return String.Join(_propertiesSeparator.ToString(), new string[] {buttonName, buttonText});
        }

        private string ConvertToLine(InputButton button)
        {
            string[] properties = {button.TextNumberKey, button.TextNumberAltKey};
            return String.Join(_propertiesSeparator.ToString(), properties);
        }

        private string[] SeparateIntoBlocks(string content)
        {
            return content.Split(new char[] { _blocksSeparator });
        }

        private Dictionary<string, InputAxis> ConvertToAxes(string block)
        {
            try
            {
                string[] lines = block.Split(new char[] { _linesSeparator });
                Dictionary<string, InputAxis> axes = new Dictionary<string, InputAxis>();
                KeyValuePair<string, InputAxis> axis;
                foreach (string line in lines)
                {
                    try
                    {
                        axis = ConvertToAxis(line);
                        axes.Add(axis.Key, axis.Value);
                    }
                    catch
                    {

                    }
                }
                return axes;
            }
            catch (IndexOutOfRangeException)
            {
                return new Dictionary<string, InputAxis>();
            }
        }

        private KeyValuePair<string, InputAxis> ConvertToAxis(string axis)
        {
            string[] properties = axis.Split(new char[] { _propertiesSeparator });
            string name = properties[0];
            InputButtonDown positiveButton = new InputButtonDown(properties[1], properties[2]);
            InputButtonDown negativeButton = new InputButtonDown(properties[3], properties[4]);
            float sensitivity = float.Parse(properties[5]);
            float gravity = float.Parse(properties[6]);
            InputAxis inputAxis = new InputAxis(positiveButton, negativeButton, sensitivity, gravity);
            return new KeyValuePair<string, InputAxis>(name, inputAxis);
        }

        private Dictionary<string, InputButtonDown> ConvertToButtons(string block)
        {
            try
            {
                string[] lines = block.Split(new char[] { _linesSeparator });
                Dictionary<string, InputButtonDown> buttons = new Dictionary<string, InputButtonDown>();
                KeyValuePair<string, InputButtonDown> button;
                foreach (string line in lines)
                {
                    try
                    {
                        button = ConvertToButton(line);
                        buttons.Add(button.Key, button.Value);
                    }
                    catch
                    {

                    }
                }
                return buttons;
            }
            catch (IndexOutOfRangeException)
            {
                return new Dictionary<string, InputButtonDown>();
            }
        }

        private KeyValuePair<string, InputButtonDown> ConvertToButton(string button)
        {
            string[] properties = button.Split(new char[] { _propertiesSeparator });
            string name = properties[0];
            InputButtonDown inputButtonDown = new InputButtonDown(properties[1], properties[2]);
            return new KeyValuePair<string, InputButtonDown>(name, inputButtonDown);
        }
    }
}


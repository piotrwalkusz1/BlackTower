using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EditorExtension;
using InputsSystem;

namespace ExtendedClassesInputsEditor
{
    static public class ExtendedList
    {
        static public Dictionary<string, InputButtonDown> ToDictionary(this List<ButtonInputsWindow> buttons)
        {
            Dictionary<string, InputButtonDown> dictionary = new Dictionary<string, InputButtonDown>();

            foreach (ButtonInputsWindow button in buttons)
            {
                dictionary.Add(button.Name, button.Button);
            }

            return dictionary;
        }

        static public Dictionary<string, InputAxis> ToDictionary(this List<AxisInputsWindow> axes)
        {
            Dictionary<string, InputAxis> dictionary = new Dictionary<string, InputAxis>();

            foreach (AxisInputsWindow axis in axes)
            {
                dictionary.Add(axis.Name, axis.Axis);
            }

            return dictionary;
        }

        static public bool ExistButtonNamed(this List<ButtonInputsWindow> list, string name)
        {
            foreach (ButtonInputsWindow button in list)
            {
                if (button.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool ExistAxisNamed(this List<AxisInputsWindow> list, string name)
        {
            foreach (AxisInputsWindow axis in list)
            {
                if (axis.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

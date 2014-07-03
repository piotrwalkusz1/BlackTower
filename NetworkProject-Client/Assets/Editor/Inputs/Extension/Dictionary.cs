using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EditorExtension;
using InputsSystem;

namespace ExtendedClassesInputsEditor
{
    static public class ExtendedDictionary
    {
        static public List<ButtonInputsWindow> ToList(this Dictionary<string, InputButtonDown> dictionary)
        {
            List<ButtonInputsWindow> list = new List<ButtonInputsWindow>();
            foreach (KeyValuePair<string, InputButtonDown> button in dictionary)
            {
                list.Add(new ButtonInputsWindow(button.Key, button.Value));
            }
            return list;
        }

        static public List<AxisInputsWindow> ToList(this Dictionary<string, InputAxis> dictionary)
        {
            List<AxisInputsWindow> list = new List<AxisInputsWindow>();
            foreach (KeyValuePair<string, InputAxis> axis in dictionary)
            {
                list.Add(new AxisInputsWindow(axis.Key, axis.Value));
            }
            return list;
        }
    }
}

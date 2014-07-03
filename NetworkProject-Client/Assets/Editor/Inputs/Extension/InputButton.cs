using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using InputsSystem;

namespace ExtendedClassesInputsEditor
{
    static public class ExtendedInputButton
    {
        static public void DrawFieldsAdjustment(this InputButton inputButton, string nameKey = "Key :",
            string nameAlternativeKey = "Alternative Key :")
        {
            string textKey = inputButton.Key.ToString();
            string textAltKey = inputButton.AltKey.ToString();
            textKey = EditorGUILayout.TextField(nameKey, textKey);
            textAltKey = EditorGUILayout.TextField(nameAlternativeKey, textAltKey);
            try
            {
                KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), textKey);
                inputButton.Key = keyCode;
            }
            catch (ArgumentException)
            {
            }
            catch (OverflowException)
            {
            }

            try
            {
                KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), textAltKey);
                inputButton.AltKey = keyCode;
            }
            catch (ArgumentException)
            {
            }
            catch (OverflowException)
            {
            }
        }
    }
}

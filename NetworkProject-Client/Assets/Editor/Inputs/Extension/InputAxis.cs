using UnityEngine;
using UnityEditor;
using System.Collections;
using InputsSystem;

namespace ExtendedClassesInputsEditor
{
    static public class ExtendedInputAxis
    {
        static public void DrawFieldsAdjustment(this InputAxis inputAxis)
        {
            inputAxis.Sensitivity = EditorGUILayout.FloatField("Sensitivity :", inputAxis.Sensitivity);
            inputAxis.Gravity = EditorGUILayout.FloatField("Gravity :", inputAxis.Gravity);
            inputAxis.PositiveButton.DrawFieldsAdjustment("Positive key :", "Positive alternative key :");
            inputAxis.NegativeButton.DrawFieldsAdjustment("Negative key :", "Negative alternative key :");
        }
    }
}

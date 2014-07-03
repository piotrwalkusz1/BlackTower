using UnityEngine;
using System.Collections;

namespace EditorExtension
{
    static public class Indentation
    {

        static public void BeginIndentation(float indentation)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(indentation);
            GUILayout.BeginVertical();
        }

        static public void EndIndentation()
        {
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}


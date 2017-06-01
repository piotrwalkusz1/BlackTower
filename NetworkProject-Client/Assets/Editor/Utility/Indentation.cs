using UnityEngine;
using System.Collections;

namespace EditorExtension
{
    static public class Indentation
    {
        private const float INDENTATION = 15f;

        static public void BeginIndentation()
        {
            BeginIndentation(INDENTATION);
        }

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


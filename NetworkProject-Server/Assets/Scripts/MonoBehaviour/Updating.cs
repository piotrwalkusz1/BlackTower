using UnityEngine;
using System;
using System.Collections;

namespace Standard
{
    [System.CLSCompliant(false)]
    public class Updating : MonoBehaviour
    {
        private float scrollPosition = 0f;
        public Texture texture;
        public GUIStyle style;

        public static event Action UpdateEvent;
        public static event Action OnGUIEvent;
        public static event Action LateUpdateEvent;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            try
            {
                UpdateEvent();
            }
            catch (System.NullReferenceException) { }
        }

        void OnGUI()
        {
            scrollPosition = GUI.VerticalScrollbar(new Rect(10, 10, 10, 400), scrollPosition, 2f, 10f, 0f, style);

            try
            {
                OnGUIEvent();
            }
            catch (System.NullReferenceException) { }         
        }

        void LateUpdate()
        {
            try
            {
                LateUpdateEvent();
            }
            catch (System.NullReferenceException) { }      
        }
    }
}


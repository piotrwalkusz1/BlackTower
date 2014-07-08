using UnityEngine;
using System;
using System.Collections;

namespace NetworkProject
{
    public class Updating : MonoBehaviour
    {
        public static event Action UpdateEvent;
        public static event Action OnGUIEvent;
        public static event Action LateUpdateEvent;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (UpdateEvent != null)
            {
                UpdateEvent();
            }
        }

        void OnGUI()
        {
            if (OnGUIEvent != null)
            {
                UpdateEvent();
            }       
        }

        void LateUpdate()
        {
            if (LateUpdateEvent != null)
            {
                LateUpdateEvent();
            }   
        }
    }
}


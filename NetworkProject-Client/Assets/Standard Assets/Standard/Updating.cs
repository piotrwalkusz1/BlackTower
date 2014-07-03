using UnityEngine;
using System;
using System.Collections;

namespace Standard
{
    public class Updating : MonoBehaviour
    {
        public static event Action UpdateEvent;
        public static event Action OnGUIEvent;
        public static event Action LateUpdateEvent;
        public static event Action<int> LevelWasLoadedEvent;
        public static MonoBehaviour MonoBehaviour { get; private set; }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            MonoBehaviour = this;
        }

        void Start()
        {
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

        void OnLevelWasLoaded(int i)
        {
            try
            {
                LevelWasLoadedEvent(i);
            }
            catch (System.NullReferenceException) { } 
        }
    }
}


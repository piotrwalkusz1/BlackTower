using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using NetworkProject;

[System.CLSCompliant(false)]
public class ApplicationController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Application.LoadLevelAdditive("Map1");
    }
}

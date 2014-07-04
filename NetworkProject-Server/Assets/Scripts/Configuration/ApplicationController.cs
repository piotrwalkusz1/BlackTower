using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using NetworkProject;
using NetworkProject.Items.Repository;

[System.CLSCompliant(false)]
public class ApplicationController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        ItemRepository.LoadItemsFromResources();

        Application.LoadLevelAdditive("Map1");
    }
}

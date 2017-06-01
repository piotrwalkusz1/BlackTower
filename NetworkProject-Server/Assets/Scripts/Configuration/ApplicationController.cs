using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Monsters;
using NetworkProject.Spells;
using NetworkProject.Buffs;

public class ApplicationController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SpellRepository.SetSpellsFromResources();
        ItemRepository.SetItemsFromResource(Standard.Settings.PATH_TO_ITEMS_IN_RESOURCE);
        MonsterRepository.SetMonstersFromResource(Standard.Settings.PATH_TO_MONSTER_IN_RESOURCE);
        BuffRepository.LoadAndSetFromResources(Standard.Settings.PATH_TO_BUFFS_IN_RESOURCES);
    }

    void LateUpdate()
    {
        UpdateVisionsAndRestetNetObjectsMessages();
    }

    public static void LoadAllLevelsAdditive()
    {
        Application.LoadLevelAdditive("Map1");
        Application.LoadLevelAdditive("Map2");
        Application.LoadLevelAdditive("Map3");
    }

    public static void LoadStartLvl()
    {
        Application.LoadLevel("Reset");
    }

    private void UpdateVisionsAndRestetNetObjectsMessages()
    {
        UpdateVisions();

        ResetNetObjectsMessages();
    }

    private void UpdateVisions()
    {
        Vision[] visions = GameObject.FindObjectsOfType<Vision>();

        foreach (Vision vision in visions)
        {
            vision.UpdateInApplicationController();
        }
    }

    private void ResetNetObjectsMessages()
    {
        NetObject[] netObjects = GameObject.FindObjectsOfType<NetObject>();

        foreach (NetObject netObject in netObjects)
        {
            netObject.ResetMessages();
        }
    }
}

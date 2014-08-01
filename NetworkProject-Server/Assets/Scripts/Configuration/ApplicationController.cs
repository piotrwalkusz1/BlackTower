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

[System.CLSCompliant(false)]
public class ApplicationController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var spellRepository = Standard.IoC.GetSpellRepository();
        spellRepository.SetSpellsFromResources();
        SpellRepository.Set(spellRepository);

        ItemRepository.SetItemsFromResource(Standard.Settings.PATH_TO_ITEMS_IN_RESOURCE);
        MonsterRepository.SetMonstersFromResource(Standard.Settings.PATH_TO_MONSTER_IN_RESOURCE);
        BuffRepository.LoadAndSetFromResources(Standard.Settings.PATH_TO_BUFFS_IN_RESOURCES);

        Application.LoadLevelAdditive("Map1");
    }

    void LateUpdate()
    {
        UpdateVisionsAndRestetNetObjectsMessages();
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

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Monsters;
using NetworkProject.Spells;

[System.CLSCompliant(false)]
public class ApplicationController : MonoBehaviour
{
    private static List<Vision> _visions = new List<Vision>();
    private static List<NetObject> _netObjects = new List<NetObject>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SpellRepository.Set(Standard.IoC.GetSpellRepository());
        ItemRepository.LoadItemsFromResources();
        MonsterRepository.LoadMonstersFromResources();

        Application.LoadLevelAdditive("Map1");
    }

    void LateUpdate()
    {
        UpdateVisionsAndRestetNetObjectsMessages();
    }

    public static void AddVision(Vision vision)
    {
        _visions.Add(vision);   
    }

    public static void AddNetObject(NetObject netObject)
    {
        _netObjects.Add(netObject);
    }

    private void UpdateVisionsAndRestetNetObjectsMessages()
    {
        UpdateVisions();

        ResetNetObjectsMessages();
    }

    private void UpdateVisions()
    {
        foreach (Vision vision in _visions)
        {
            vision.UpdateInApplicationController();
        }
    }

    private void ResetNetObjectsMessages()
    {
        foreach (NetObject netObject in _netObjects)
        {
            netObject.ResetMessages();
        }
    }
}

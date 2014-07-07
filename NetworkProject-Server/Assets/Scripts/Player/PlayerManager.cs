﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Spells;
using NetworkProject.Items;

public class PlayerManager : MonoBehaviour
{
    public string Name { get; private set; }

    public void Initialize(int idNet, RegisterCharacter characterData, IConnectionMember ownerAddress)
    {
        Name = characterData.Name;

        InitializeNetPlayer(idNet, ownerAddress);

        InitalizeVision(ownerAddress);

        InitializeStats(characterData);

        InitializeSpellCaster(characterData.Spells);

        InitializePlayerExperience(characterData.Lvl, characterData.Exp);

        InitializePlayerEquipment();

        GetComponent<PlayerStats>().CalculateStats(); // musi być przedostatnie!

        GetComponent<PlayerHealthSystem>().Recuparate(); // musi być na końcu!
    }

    private void InitializeNetPlayer(int idNet, IConnectionMember ownerAddress)
    {
        var netPlayer = GetComponent<NetPlayer>();
        netPlayer.IdNet = idNet;
        netPlayer.OwnerAddress = ownerAddress;
    }

    private void InitalizeVision(IConnectionMember ownerAddress)
    {
        var vision = GetComponent<Vision>();
        vision.AddObserver(ownerAddress);
    }

    private void InitializeStats(RegisterCharacter characterData)
    {
        var stats = GetComponent<PlayerStats>();
        stats.Set(characterData);
    }

    private void InitializeSpellCaster(List<Spell> spells)
    {
        var spellCaster = GetComponent<SpellCaster>();
        spellCaster.SetSpells(spells);
    }

    private void InitializePlayerExperience(int lvl, int exp)
    {
        var playerExperience = GetComponent<PlayerExperience>();
        playerExperience.Set(lvl, exp);
    }

    private void InitializePlayerEquipment()
    {
        var playerEquipment = GetComponent<PlayerEquipment>();

        playerEquipment.AddItem(new Item(0)); //temporary
        playerEquipment.AddItem(new Item(1)); //temporary
        playerEquipment.AddItem(new Item(2)); //temporary
    }
}
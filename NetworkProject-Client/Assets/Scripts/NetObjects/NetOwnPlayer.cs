using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;

public class NetOwnPlayer : NetPlayer
{
    private PlayerHealth _health;
    private DateTime _nextCheckMovementAndRotation;
    private float _checkMovementAndRotationRate = 0.05f;

    protected new void Awake()
    {
        base.Awake();

        _health = GetComponent<PlayerHealth>();

        CheckChangePositionAndRotation();
        _nextCheckMovementAndRotation = DateTime.UtcNow.AddSeconds(_checkMovementAndRotationRate);
    }

	protected void Update()
    {
        if (DateTime.UtcNow > _nextCheckMovementAndRotation)
        {
            CheckChangePositionAndRotation();
            _nextCheckMovementAndRotation = DateTime.UtcNow.AddSeconds(_checkMovementAndRotationRate);

            if (_health.IsAlive())
            {
                if (IsMovement())
                {
                    var request = new PlayerMoveToServer(transform.position);

                    Client.SendRequestAsMessage(request);
                }

                if (IsRotation())
                {
                    var request = new PlayerRotationToServer(transform.eulerAngles.y);

                    Client.SendRequestAsMessage(request);
                }
            }
        }   
	}

    public void InitializePlayer(CreateOwnPlayerToClient package)
    {
        IdNet = package.IdNet;
        IsModelVisible = package.IsModelVisible;
        Name = package.Name;

        var stats = GetComponent<OwnPlayerStats>();
        package.PlayerStats.CopyToStats(stats);

        var eq = GetComponent<OwnPlayerEquipment>();
        eq.SetEquipmentBag(package.Equipment);
        eq.SetEquipedItems(package.EquipedItems);

        var spellCaster = GetComponent<SpellCaster>();
        spellCaster.SetSpells(PackageConverter.PackageToSpell(package.Spells.ToArray()).ToArray());
    }
}
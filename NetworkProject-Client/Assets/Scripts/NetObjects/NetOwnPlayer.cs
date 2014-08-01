using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;

public class NetOwnPlayer : NetPlayer
{
	protected void Update()
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

    public void InitializePlayer(CreateOwnPlayerToClient package)
    {
        IdNet = package.IdNet;
        IsModelVisible = package.IsModelVisible;
        Name = package.Name;

        var stats = GetComponent<OwnPlayerStats>();
        package.PlayerStats.CopyToStats(stats);

        var eq = GetComponent<OwnPlayerEquipment>();
        eq.SetItemsInBag(package.Equipment.GetItems());
        eq.SetEquipedItems(package.EquipedItems);

        var spellCaster = GetComponent<SpellCaster>();
        List<SpellClient> spells = new List<SpellClient>();
        package.Spells.ForEach(x => spells.Add(new SpellClient(x)));
        spellCaster.SetSpells(spells.ToArray());
    }
}

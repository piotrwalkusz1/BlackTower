using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.BodyParts;
using NetworkProject.Items;
using NetworkProject.Spells;

[System.CLSCompliant(false)]
public class NetPlayer : NetObject
{
    public IConnectionMember OwnerAddress { get; set; }
    public string Name { get; set; }
    public BreedAndGender BreadAndGender { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        if(OwnerAddress.Equals(address))
        {
            return;
        }

        var stats = GetComponent<PlayerStats>();
        var request = new CreateOtherPlayerToClient(IdNet, transform.position, transform.eulerAngles.y, stats, Name);
        var message = new OutgoingMessage(request);

        Server.Send(message, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        if (OwnerAddress.Equals(address))
        {
            return;
        }

        base.SendMessageUpdate(address);
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        if (OwnerAddress.Equals(address))
        {
            return;
        }

        base.SendMessageDisappeared(address);
    }

    public void SendJumpMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new JumpToClient(IdNet);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendAttackMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new AttackToClient(IdNet);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendChangeAllStatsMessage()
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new UpdateAllStatsToClient(IdNet, GetComponent<PlayerStats>());
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendUpdateEquipedItem(int slot, Item item)
    {
        Action<IConnectionMember> function = delegate(IConnectionMember address)
        {
            var request = new UpdateEquipedItemToClient(IdNet, slot, item);
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendRespawnMessageToOwner()
    {
        var request = new RespawnToClient(IdNet, transform.position);

        Server.SendRequestAsMessage(request, OwnerAddress);
    }

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

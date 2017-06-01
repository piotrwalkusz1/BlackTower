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
        var eq = GetComponent<PlayerEquipment>();
        var request = new CreateOtherPlayerToClient(IdNet, IsModelVisible, transform.position, transform.eulerAngles.y, stats, Name,
            PackageConverter.PlayerEquipedItemsToPackage(eq));
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
            var request = new UpdateEquipedItemToClient(IdNet, slot, PackageConverter.ItemToPackage(item));
            var message = new OutgoingMessage(request);

            Server.Send(message, address);
        };

        _sendMessageUpdateEvent += function;
    }

    public void SendChatMessageToOwner(string sender, string message)
    {
        var request = new ChatMessageToClient(sender + " : " + message);

        Server.SendRequestAsMessage(request, OwnerAddress);
    }

    public void SendRespawnMessageToOwner()
    {
        var request = new RespawnToClient(IdNet, transform.position);

        Server.SendRequestAsMessage(request, OwnerAddress);
    }

    public void SendCreateMessageToOwner()
    {
        var tran = transform;
        var stats = GetComponent<PlayerStats>();
        var eq = GetComponent<PlayerEquipment>();
        var spellCaster = GetComponent<SpellCaster>();
        var createOwnPlayerRequest = new CreateOwnPlayerToClient(IdNet, IsModelVisible,
            tran.position, tran.eulerAngles.y, stats, Name, PackageConverter.EquipmentToPackage(eq),
            PackageConverter.PlayerEquipedItemsToPackage(eq), PackageConverter.SpellToPackage(spellCaster.GetSpells()));

        Server.SendRequestAsMessage(createOwnPlayerRequest, OwnerAddress);
    }

    public void Initialize(int idNet, RegisterCharacter characterData, IConnectionMember ownerAddress)
    {
        Name = characterData.Name;

        InitializeNetPlayer(idNet, ownerAddress);

        InitalizeVision(ownerAddress);       

        InitializeSpellCaster(PackageConverter.PackageToSpell(characterData.Spells.ToArray()));

        InitializePlayerEquipment(characterData.Equipment, characterData.EquipedItems);

        InitializeStats(characterData);

        InitializeQuests(characterData.CurrentQuests, characterData.ReturnedQuests);
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
        characterData.Stats.CopyToStats(stats);

        stats.CalculateStats();
    }

    private void InitializeSpellCaster(List<Spell> spells)
    {
        var spellCaster = GetComponent<SpellCaster>();
        spellCaster.SetSpells(spells);
    }

    private void InitializePlayerEquipment(EquipmentPackage equipment, PlayerEquipedItemsPackage equipedItems)
    {
        var playerEquipment = GetComponent<PlayerEquipment>();

        playerEquipment.Set(equipment, equipedItems);
    }

    private void InitializeQuests(List<Quest> currentQuests, List<int> returnedQuests)
    {
        var questExecutor = GetComponent<QuestExecutor>();

        questExecutor.SetQuests(currentQuests == null ? new Quest[0] : currentQuests.ToArray(),
            returnedQuests == null ? new int[0] : returnedQuests.ToArray());

        questExecutor.InitializeAllQuests();
    }
}

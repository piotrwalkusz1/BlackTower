using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Items;

[Serializable]
public class RegisterCharacter
{
    public RegisterAccount MyAccount { get; set; }
    public string Name { get; set; }
    public int IdCharacter { get; set; }
    public BreedAndGender BreedAndGender { get; set; }

    public Vector3Serializable EndPosition { get; set; } // position at time of exit the game  
    public List<SpellPackage> Spells { get; set; }
    public EquipmentPackage Equipment { get; set; }
    public PlayerEquipedItemsPackage EquipedItems { get; set; }
    public PlayerStatsPackage Stats { get; set; }
    public List<Quest> CurrentQuests { get; set; }
    public List<int> ReturnedQuests { get; set; }
    public List<HotkeysPackage> Hotkeys { get; set; }

    public RegisterCharacter(string name, BreedAndGender breedAndGender)
    {
        Name = name;
        BreedAndGender = breedAndGender;

        Spells = new List<SpellPackage>();
        Equipment = GetEmptyEquipmentPackage();
        EquipedItems = new PlayerEquipedItemsPackage(IoC.GetBodyParts().ToList());
        Stats = new PlayerStatsPackage();
        CurrentQuests = new List<Quest>();
        ReturnedQuests = new List<int>();
        Hotkeys = new List<HotkeysPackage>();
        for (int i = 0; i < 10; i++)
        {
            Hotkeys.Add(null);
        }

        Stats.Lvl = 1;

        Standard.Settings.StatsToDefault(Stats);

        Stats.HP = Stats.MaxHP;
        Stats.Mana = Stats.MaxMana;
    }

    public void Update(GameObject player)
    {
        EndPosition = player.transform.position;
        Spells = PackageConverter.SpellToPackage(player.GetComponent<SpellCaster>().GetSpells());

        var playerEquipment = player.GetComponent<PlayerEquipment>();
        Equipment = PackageConverter.EquipmentToPackage(playerEquipment);
        EquipedItems = PackageConverter.PlayerEquipedItemsToPackage(playerEquipment);

        var stats = player.GetComponent<PlayerStats>();
        Stats.CopyFromStats(stats);

        var questExecutor = player.GetComponent<QuestExecutor>();
        CurrentQuests = new List<Quest>(questExecutor.GetCurrentQuests());
        ReturnedQuests = new List<int>(questExecutor.GetReturnedQuest());
    }

    public void AddSpell(Spell spell)
    {
        Spells.Add(PackageConverter.SpellToPackage(spell));
    }

    protected EquipmentPackage GetEmptyEquipmentPackage()
    {
        var items = new List<ItemPackage>();

        for (int i = 0; i < NetworkProject.Settings.widthEquipment * NetworkProject.Settings.heightEquipment; i++)
        {
            items.Add(null);
        }

        return new EquipmentPackage(items.ToArray(), 0);
    }
}

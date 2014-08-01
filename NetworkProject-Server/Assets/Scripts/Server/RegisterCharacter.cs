using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public class RegisterCharacter
{
    public RegisterAccount MyAccount { get; set; }
    public string Name { get; set; }
    public int IdCharacter { get; set; }
    public BreedAndGender BreedAndGender { get; set; }

    public Vector3 EndPosition { get; set; } // position at time of exit the game  
    public List<Spell> Spells { get; set; }
    public Equipment Equipment { get; set; }
    public PlayerEquipedItems EquipedItems { get; set; }
    public int Exp { get; set; }
    public int Lvl { get; set; }

    public RegisterCharacter()
    {
        Spells = new List<Spell>();
        Equipment = new Equipment(NetworkProject.Settings.widthEquipment * NetworkProject.Settings.heightEquipment);
        EquipedItems = new PlayerEquipedItems();

        Lvl = 1;
    }

    public void Update(GameObject player)
    {
        EndPosition = player.transform.position;
        Spells = new List<Spell>(player.GetComponent<SpellCaster>().GetSpells());

        var playerEquipment = player.GetComponent<PlayerEquipment>();
        Equipment = playerEquipment.GetEquipment();
        EquipedItems = playerEquipment.GetEquipedItems();

        var experience = player.GetComponent<PlayerExperience>();
        Exp = experience.Exp;
        Lvl = experience.Lvl;
    }

    public void AddSpell(Spell spell)
    {
        Spells.Add(spell);
    }
}

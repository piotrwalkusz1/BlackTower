using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;

[System.CLSCompliant(false)]
public class RegisterCharacter
{
    public RegisterAccount MyAccount { get; set; }
    public string Name { get; set; }
    public int IdCharacter { get; set; }
    public Breed Breed { get; set; }

    public Vector3 EndPosition { get; set; } // position at time of exit the game  
    public List<Spell> Spells { get; set; }
    public int Exp { get; set; }
    public int Lvl { get; set; }

    public RegisterCharacter()
    {
        Spells = new List<Spell>();
    }

    public void Update(NetPlayer player)
    {
        EndPosition = player.transform.position;
        Spells = new List<Spell>(player.GetComponent<SpellCaster>().GetSpells());

        var experience = player.GetComponent<PlayerExperience>();
        Exp = experience.Exp;
        Lvl = experience.Lvl;
    }

    public void AddSpell(Spell spell)
    {
        Spells.Add(spell);
    }
}

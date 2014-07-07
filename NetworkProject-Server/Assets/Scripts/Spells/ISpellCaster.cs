using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Spells;

[System.CLSCompliant(false)]
public interface ISpellCaster
{
    int Lvl { get; }
    int Mana { get; }
    Vector3 Position { get; }
    float Rotation { get; }
    ISpellCasterStats Stats { get; }

    void AddSpell(Spell spell);

    Spell[] GetSpells();

    bool TryCastSpellFromSpellBook(int idSpell, params ISpellCastOption[] options);
}


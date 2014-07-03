using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;

[System.CLSCompliant(false)]
public interface ISpellCaster
{
    int Lvl { get; }
    int Mana { get; }
    Vector3 Position { get; }
    float Rotation { get; }

    void AddSpell(Spell spell);

    Spell[] GetSpells();

    void CastSpell(Spell spell);

    void CastSpell(Spell spell, params ISpellCastOption[] options);

    void CastSpell(Spell spell, out string reason, params ISpellCastOption[] options);
}


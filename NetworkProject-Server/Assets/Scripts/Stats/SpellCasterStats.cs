using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Connection;
using UnityEngine;

public class SpellCasterStats : Stats, ISpellCasterStats
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using UnityEngine;

public class SpellCasterStats : MonsterRespawn, ISpellCasterStats
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }
}

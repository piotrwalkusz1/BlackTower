using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.CLSCompliant(false)]
public interface ISpellCaster
{
    int Lvl { get; }
    int Mana { get; }
}

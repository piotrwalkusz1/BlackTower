using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerKillInfo : KillInfo
{
    public int IdCharacter { get; set; }

    public PlayerKillInfo(int idCharacter)
    {
        IdCharacter = idCharacter;
    }
}

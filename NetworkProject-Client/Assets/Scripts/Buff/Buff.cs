using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;

public class Buff
{
    public int IdBuff { get; set; }
    public int LvlBuff { get; set; }
    public DateTime EndTime { get; set; }
    public string Name
    {
        get { return Languages.GetBuffName(IdBuff); }
    }
    public string Description
    {
        get { return Languages.GetBuffDescription(IdBuff); }
    }

    public Buff(int idBuff, int lvlBuff, DateTime endTime)
    {
        IdBuff = idBuff;
        LvlBuff = lvlBuff;
        EndTime = endTime;
    }
}

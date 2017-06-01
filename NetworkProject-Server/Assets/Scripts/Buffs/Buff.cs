using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Benefits;
using NetworkProject.Buffs;
using UnityEngine;

public class Buff
{
    public int IdBuff { get; protected set; }
    public int LvlBuff { get; set; }
    public DateTime EndTime { get; protected set; }
    public BuffData BuffData
    {
        get { return BuffRepository.GetBuff(IdBuff); }
    }

    public List<GameObject> BoundGameObjects { get; set; }

    public Buff(int idBuff, int lvlBuff, DateTime endTime)
    {
        IdBuff = idBuff;
        LvlBuff = lvlBuff;
        EndTime = endTime;

        BoundGameObjects = new List<GameObject>();
    }

    public void OnCast(IBuffable target)
    {
        var function = BuffActionRepository.GetOnCast(IdBuff);

        function(this, target);
    }

    public void OnEnd(IBuffable target)
    {
        var function = BuffActionRepository.GetOnEnd(IdBuff);

        function(this, target);

        foreach (var obj in BoundGameObjects)
        {
            MonoBehaviour.Destroy(obj);
        }
    }

    public void ApplyToStats(IPlayerStats stats)
    {
        foreach (var benefit in BuffData.Benefits[LvlBuff])
        {
            benefit.ApplyToStats(stats);
        }
    }
} 

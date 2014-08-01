using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Benefits;
using NetworkProject.Buffs;
using UnityEngine;

[Serializable]
public class BuffServer : Buff
{
    public List<GameObject> BoundGameObjects { get; set; }

    public BuffServer(int idBuff, DateTime endTime)
        : base(idBuff, endTime)
    {
        BoundGameObjects = new List<GameObject>();
    }

    public void OnCast(IBuffableServer target)
    {
        var function = BuffActionRepository.GetOnCast(IdBuff);

        function(this, target);
    }

    public void OnEnd(IBuffableServer target)
    {
        var function = BuffActionRepository.GetOnEnd(IdBuff);

        function(this, target);

        foreach (var obj in BoundGameObjects)
        {
            MonoBehaviour.Destroy(obj);
        }
    }
} 

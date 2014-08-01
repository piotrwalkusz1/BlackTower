using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Benefits;
using UnityEngine;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class Buff
    {
        public int IdBuff { get; set; }

        public List<IBenefit> Benefits { get; set; }

        public DateTime EndTime { get; protected set; }

        public Buff(int idBuff, DateTime endTime)
        {
            IdBuff = idBuff;
            EndTime = endTime;
            Benefits = new List<IBenefit>();
        }

        public void ApplyToStats(IPlayerStats stats)
        {
            foreach (var benefit in Benefits)
            {
                benefit.ApplyToStats(stats);
            }
        }

        public bool IsEndTime()
        {
            return DateTime.UtcNow > EndTime;
        }
    } 
}

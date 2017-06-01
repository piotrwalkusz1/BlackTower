using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToServer;
using NetworkProject.Quests;

public class Combat : MonoBehaviour
{
    public event Action<KillInfo, GameObject> OnKill;

    public void OnKillInvoke(KillInfo killInfo)
    {
        if (OnKill != null)
        {
            OnKill(killInfo, gameObject);
        }  
    }
}

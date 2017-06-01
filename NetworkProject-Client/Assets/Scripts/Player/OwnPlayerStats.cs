using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public class OwnPlayerStats : PlayerStats
{
    public int MaxExp
    {
        get { return GetComponent<PlayerExperience>().MaxExp; }
        set { GetComponent<PlayerExperience>().MaxExp = value; }
    }
}

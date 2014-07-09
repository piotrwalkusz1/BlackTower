using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public class OtherPlayerStats : PlayerStats
{
    public override float MovementSpeed
    {
        get
        {
            return GetComponent<PlayerMovement>().Speed;
        }
        set
        {
            GetComponent<PlayerMovement>().Speed = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToClient;

public abstract class Combat : MonoBehaviour
{
    public abstract void Attack(AttackToClient attackInfo);
}

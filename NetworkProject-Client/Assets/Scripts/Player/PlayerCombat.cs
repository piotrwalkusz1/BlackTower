using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerCombat : MonoBehaviour
{
    public float AttackSpeed { get; set; }

    public void Attack()
    {
        SendMessage("OnAttack1");
    }
}

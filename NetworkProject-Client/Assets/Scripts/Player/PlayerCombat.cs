using UnityEngine;
using System.Collections;
using NetworkProject;

public class PlayerCombat : MonoBehaviour
{
    public float AttackSpeed { get; set; }
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }

    public virtual void Attack()
    {
        GetComponent<PlayerAnimation>().Attack();
    }
}

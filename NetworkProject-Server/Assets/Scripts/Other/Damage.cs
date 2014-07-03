using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class Damage : MonoBehaviour
{
    public AttackInfo _attackInfo;
    public GameObject _attacker;

    void OnTriggerEnter(Collider hit)
    {
        if (_attacker != hit.gameObject)
        {
            HealthSystem hp = hit.GetComponentInChildren<HealthSystem>();

            hp.Attack(_attackInfo);

            Destroy(gameObject);
        }       
    }
}

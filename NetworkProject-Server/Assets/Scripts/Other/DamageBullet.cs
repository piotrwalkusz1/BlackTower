using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class DamageBullet : MonoBehaviour
{
    public AttackInfo _attackInfo;

    void OnTriggerEnter(Collider hit)
    {
        HealthSystem hp = hit.GetComponentInChildren<HealthSystem>();

        hp.AttackAndSendUpdate(_attackInfo);

        Destroy(gameObject);    
    }
}

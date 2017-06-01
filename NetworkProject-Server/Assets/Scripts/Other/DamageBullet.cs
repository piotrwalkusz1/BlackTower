using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DamageBullet : MonoBehaviour
{
    public AttackInfo _attackInfo;

    public List<GameObject> _insensitive = new List<GameObject>();

    void OnTriggerEnter(Collider hit)
    {
        if (!_insensitive.Contains(hit.gameObject))
        {
            HealthSystemBase hp = hit.GetComponentInChildren<HealthSystemBase>();

            if (hp != null)
            {
                hp.AttackAndSendUpdate(_attackInfo);
            }

            Destroy(gameObject);   
        }
    }
}

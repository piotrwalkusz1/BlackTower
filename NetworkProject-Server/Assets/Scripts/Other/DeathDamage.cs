using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DeathDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider hit)
    {
        HealthSystemBase hp = hit.GetComponent<HealthSystemBase>();

        if (hp != null)
        {
            hp.DieAndSendUpdate();
        }
        else
        {
            ExplosionDamage explosion = hit.GetComponent<ExplosionDamage>();

            if (explosion != null)
            {
                explosion.Explosion();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public AttackInfo AttackInfo { get; set; }
    public float _explosionRange;
    public DateTime EndTime { get; set; }
    public int _explosionIdVisualObject;

    void OnCollisionEnter()
    {
        Explosion();
    }

    void Update()
    {
        if (DateTime.UtcNow > EndTime)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRange);

        HealthSystemBase hp;

        foreach(var hit in hits)
        {
            hp = hit.GetComponent<HealthSystemBase>();

            if (hp != null)
            {
                hp.AttackAndSendUpdate(AttackInfo);
            }
        }

        GameObject explosion = SceneBuilder.CreateVisualObject(_explosionIdVisualObject, transform.position, 0f);

        Destroy(explosion, 2f);

        SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);

        transform.position = new Vector3(10000, 10000, 10000);

        Destroy(gameObject, 1f);
    }
}

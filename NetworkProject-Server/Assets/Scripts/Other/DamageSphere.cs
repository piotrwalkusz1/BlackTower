using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    public float Radius { get; set; }
    public IAttacker Attacker { get; set; }
    public DamageType DamageType { get; set; }
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }
    public float Cooldown { get; set; }

    public List<HealthSystem> Insenitive { get; set; }

    private DateTime _nextDamageTime = DateTime.UtcNow;

    public void Awake()
    {
        Insenitive = new List<HealthSystem>();
    }

    public void Update()
    {
        if (DateTime.UtcNow > _nextDamageTime)
        {
            _nextDamageTime = DateTime.UtcNow.AddSeconds(Cooldown);

            var colliders = Physics.OverlapSphere(transform.position, Radius);

            foreach (var hit in colliders)
            {
                HealthSystem hp = hit.gameObject.GetComponent<HealthSystem>();
                if (hp != null && !Insenitive.Exists(x => x == hp))
                {
                    int dmg = UnityEngine.Random.Range(MinDmg, MaxDmg);

                    AttackInfo attackInfo = new AttackInfo(Attacker, dmg, DamageType);

                    hp.AttackAndSendUpdate(attackInfo);
                }
            }
        }
    }
}

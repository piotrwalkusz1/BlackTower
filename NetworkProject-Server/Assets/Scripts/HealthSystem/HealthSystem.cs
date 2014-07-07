using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class HealthSystem : MonoBehaviour
{
    public event Func<AttackInfo, HealthSystem, AttackInfo> AttackEvent;

    public int MaxHP { get; private set; }
    public int HP
    {
        get
        {
            return Mathf.CeilToInt(_hp);
        }
    }
    public float HPRegenerationPerSecond { get; set; }

    private float _hp;

    protected void Awake()
    {
        _hp = (float)MaxHP;       

        HPRegenerationPerSecond = 0f;
    }

    void Update()
    {
        Regeneration();
    }

    public bool IsDead()
    {
        return HP <= 0;
    }

    public void ChangeHp(int hp)
    {
        _hp = (float)hp;
    }

    public virtual void Attack(AttackInfo attackInfo)
    {
        attackInfo = ApplyAttackEvent(attackInfo);

        DecreaseHP(attackInfo.Dmg);
    }

    protected AttackInfo ApplyAttackEvent(AttackInfo attackInfo)
    {
        if(AttackEvent != null)
        foreach (Func<AttackInfo, HealthSystem, AttackInfo> function in AttackEvent.GetInvocationList())
        {
            attackInfo = function(attackInfo, this);
        }

        return attackInfo;
    }

    public void ChangeMaxHP(int maxHp)
    {
        MaxHP = maxHp;      

        if (_hp > MaxHP)
        {
            _hp = MaxHP;
        }
    }

    public void IncreaseHP(float hp)
    {
        _hp += hp;

        if (_hp > MaxHP)
        {
            _hp = MaxHP;
        }
    }

    public void DecreaseHP(float hp)
    {
        _hp -= hp;

        CheckWhetherIsDead();
    }

    public virtual void DieAndSendUpdate()
    {
        SendMessage("OnDead");

        NetObject netObject = GetComponent<NetObject>();

        if (netObject!= null)
        {
            netObject.SendDeadMessage();
        }

        transform.position = new Vector3(1000000, 1000000, 1000000);

        Destroy(gameObject, 1f);
    }

    public virtual void SendUpdateHP()
    {
        SendUpdateHPToOthers();
    }

    public void SendUpdateHPToOthers()
    {
        NetObject netObject = GetComponent<NetObject>();

        netObject.SendChangeHpMessage();
    }

    public void Recuparate()
    {
        _hp = (float)MaxHP;
    }

    protected void CheckWhetherIsDead()
    {
        if (IsDead())
        {
            DieAndSendUpdate();
        }
    }

    protected void Regeneration()
    {
        float hpIncrease = HPRegenerationPerSecond * Time.deltaTime;

        IncreaseHP(hpIncrease);
    } 
}

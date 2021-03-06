﻿using UnityEngine;
using System;
using System.Collections;

public abstract class HealthSystem : HealthSystemBase
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
    protected bool _isSendedDeadMessage;

    protected void Start()
    {

    }

    protected void Update()
    {
        if (!IsDead())
        {
            RegenerationAndSendUpdate();
        }      
    }

    public bool IsDead()
    {
        return HP <= 0;
    }

    public void ChangeHp(int hp)
    {
        _hp = (float)hp;
    }

    public override void AttackWithoutSendUpdate(AttackInfo attackInfo)
    {
        if (!IsDead())
        {
            attackInfo = ApplyAttackEvent(attackInfo);

            DecreaseHP(attackInfo.Dmg);

            if (IsDead() && attackInfo.Attacker is Attacker)
            {               
                var attacker = (Attacker)attackInfo.Attacker;

                attacker._attackerObject.GetComponent<Combat>().OnKillInvoke(GetKillInfo());
            }
        }       
    }

    public override void AttackAndSendUpdate(AttackInfo attackInfo)
    {
        AttackWithoutSendUpdate(attackInfo);

        SendUpdateHP();
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

    public override void DieAndSendUpdate()
    {
        SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);

        ChangeHp(0);

        if (!_isSendedDeadMessage)
        {
            NetObject netObject = GetComponent<NetObject>();

            if (netObject != null)
            {
                netObject.SendDeadMessage();
            }
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
        Recuparate(MaxHP);
    }

    public void Recuparate(int hp)
    {
        _hp = hp;

        _isSendedDeadMessage = false;
    }

    protected virtual void Die()
    {
        SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);

        ChangeHp(0);

        transform.position = new Vector3(1000000, 1000000, 1000000);

        Destroy(gameObject, 1f);
    }

    protected abstract KillInfo GetKillInfo();

    protected void CheckWhetherIsDead()
    {
        if (IsDead() && !_isSendedDeadMessage)
        {
            DieAndSendUpdate();

            _isSendedDeadMessage = true;
        }
    }

    protected void RegenerationWithoutSendUpdate()
    {
        if (!IsDead())
        {
            float hpIncrease = HPRegenerationPerSecond * Time.deltaTime;

            IncreaseHP(hpIncrease);
        }
        else
        {
            MonoBehaviour.print("Obiekt jest martwy. Nie można zregenerować.");
        }
    }

    protected void RegenerationAndSendUpdate()
    {
        int hp = HP;

        RegenerationWithoutSendUpdate();

        if (hp != HP)
        {
            SendUpdateHP();
        }
    } 
}

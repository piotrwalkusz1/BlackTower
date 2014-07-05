using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class JumperAI : MonoBehaviour
{
    private const float FindRange = 15f;
    private const float AttactRange = 2f;
    private const float TimeBetweenAttacks = 1.5f;
    private const float _timeBetweenSearchings = 3f;

    private int _minDmg = 0;
    private int _maxDmg = 1;

    private DateTime _lastAttackTime;

    private JumperMovement _movement;
    private NetMonster _netMonster;
    private NetPlayer _targetPlayer;
    private DateTime _nextSearchingTime = DateTime.UtcNow;

	void Awake() 
    {
        MonsterInfo monster = MonsterRepository.GetMonsterInfo(MonsterType.Jumper);
        _minDmg = monster._damages[0];
        _maxDmg = monster._damages[1];

        InitializeItemsToDrop(monster);

        _lastAttackTime = DateTime.UtcNow;

        _movement = GetComponent<JumperMovement>();
        _netMonster = GetComponent<NetMonster>();
	}
	
	void Update()
    {
        if (DateTime.UtcNow > _nextSearchingTime)
        {
            FindTargetPlayer();
            _nextSearchingTime = DateTime.UtcNow.AddSeconds(_timeBetweenSearchings);
        }

        else if (_targetPlayer != null && _targetPlayer.IsDead())
        {
            FindTargetPlayer();
        }
            

        else if (_targetPlayer != null)
        {
            ChaseAndAttackPlayer();
        }
	}

    void OnDead()
    {
        enabled = false; 

    }

    private void FindTargetPlayer()
    {
        NetPlayer[] players = GameObject.FindObjectsOfType(typeof(NetPlayer)) as NetPlayer[];

        float nearest = FindRange;
        NetPlayer nearestPlayer = null;

        foreach (NetPlayer player in players)
        {
            if (player.IsDead())
            {
                continue;
            }

            Vector3 vector = player.transform.position - transform.position;

            if (vector.sqrMagnitude < nearest * nearest)
            {
                nearest = vector.magnitude;
                nearestPlayer = player;
            }
        }

        _targetPlayer = nearestPlayer;
    }

    private void ChaseAndAttackPlayer()
    {
        Vector3 offset = transform.position - _targetPlayer.transform.position;

        if (offset.sqrMagnitude <= AttactRange * AttactRange)
        {
            Stop();

            TryAttack();
        }
        else
        {
            Chase();
        }
    }

    private void TryAttack()
    {
        if (CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        int dmg = UnityEngine.Random.Range(_minDmg, _maxDmg);

        Attacker attacker = new Attacker(gameObject);

        AttackInfo attackInfo = new AttackInfo(attacker, dmg, DamageType.Physical);

        HealthSystem healtSystem = _targetPlayer.GetComponent<HealthSystem>();
        healtSystem.Attack(attackInfo);
        healtSystem.SendHpUpdating();

        _lastAttackTime = DateTime.UtcNow;

        _netMonster.SendAttackMessage(_targetPlayer.IdNet);
    }

    private void Chase()
    {
        _movement.SetNewPosition(_targetPlayer.transform.position);
    }

    private bool CanAttack()
    {
        TimeSpan offset =  DateTime.UtcNow - _lastAttackTime;

        if (offset.TotalSeconds >= TimeBetweenAttacks)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Stop()
    {
        _movement.Stop();
    }

    private void InitializeItemsToDrop(MonsterInfo monster)
    {
        Drop drop = GetComponent<Drop>();

        for (int i = 0; i < monster._itemsToDrop.Count; i++)
        {
            drop.AddItemToDrop(new ItemDrop(monster._itemsToDrop[i], monster._chancesToDrop[i]));
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Monsters;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public class JumperAI : MonoBehaviour
{
    private const float FIND_RANGE = 15f;
    private const float TIME_BETWEEN_SEARCHING = 3f;

    private float _attackRange;
    private float _attackSpeed;
    

    private int _minDmg = 0;
    private int _maxDmg = 1;

    private DateTime _nextAttackTime = DateTime.UtcNow;

    private JumperMovement _movement;
    private NetMonster _netMonster;
    private NetPlayer _targetPlayer;
    private DateTime _nextSearchingTime = DateTime.UtcNow;

	void Awake() 
    {
        _attackSpeed = 1.5f;
        _attackRange = 2f;

        Monster monster = MonsterRepository.GetMonster(GetComponent<NetMonster>().IdMonster);
        _minDmg = monster._damages[0];
        _maxDmg = monster._damages[1];

        InitializeItemsToDrop(monster);

        _movement = GetComponent<JumperMovement>();
        _netMonster = GetComponent<NetMonster>();
	}
	
	void Update()
    {
        if (DateTime.UtcNow > _nextSearchingTime)
        {
            FindTargetPlayer();
            _nextSearchingTime = DateTime.UtcNow.AddSeconds(TIME_BETWEEN_SEARCHING);
        }

        if (_targetPlayer != null)
        {
            if (_targetPlayer.GetComponent<PlayerHealthSystem>().IsDead())
            {
                FindTargetPlayer();
            }

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

        float nearest = FIND_RANGE;
        NetPlayer nearestPlayer = null;

        foreach (NetPlayer player in players)
        {
            if (player.GetComponent<PlayerHealthSystem>().IsDead())
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

        if (offset.sqrMagnitude <= _attackRange * _attackRange)
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
        healtSystem.SendUpdateHPToOthers();

        _nextAttackTime = DateTime.UtcNow.AddSeconds(_attackSpeed);

        _netMonster.SendAttackMessage(_targetPlayer.IdNet);
    }

    private void Chase()
    {
        _movement.SetNewPosition(_targetPlayer.transform.position);
    }

    private bool CanAttack()
    {
        return DateTime.UtcNow > _nextAttackTime;
    }

    private void Stop()
    {
        _movement.Stop();
    }

    private void InitializeItemsToDrop(Monster monster)
    {
        Drop drop = GetComponent<Drop>();

        drop.AddItemToDrop(monster._drop.ToArray());
    }
}

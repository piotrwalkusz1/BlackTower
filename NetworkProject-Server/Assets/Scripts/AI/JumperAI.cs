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

    private DateTime _nextSearchingTime = DateTime.UtcNow;
    private NetPlayer _targetPlayer;

    private JumperMovement _movement;
    private NetMonster _netMonster;  
    private MonsterCombat _combat;

	void Awake() 
    {
        _movement = GetComponent<JumperMovement>();
        _netMonster = GetComponent<NetMonster>();
        _combat = GetComponent<MonsterCombat>();

        MonsterFullData monster = (MonsterFullData)MonsterRepository.GetMonster(GetComponent<NetMonster>().IdMonster);

        var stats = GetComponent<MonsterStats>();
        monster.Stats.CopyToStats(stats);

        InitializeItemsToDrop(monster);    
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
        if (_combat.IsEnoughDistanceToAttack(_targetPlayer.transform.position))
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
        if (_combat.IsEnoughTimeToAttack())
        {
            _combat.Attack(_targetPlayer.GetComponent<HealthSystem>());
        }
    }

    private void Chase()
    {
        _movement.SetNewPosition(_targetPlayer.transform.position);
    }

    private void Stop()
    {
        _movement.Stop();
    }

    private void InitializeItemsToDrop(MonsterFullData monster)
    {
        Drop drop = GetComponent<Drop>();

        drop.AddItemToDrop(monster.Drop.ToArray());
    }
}

using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Monsters;
using NetworkProject.Items;

public class JumperAI : MonsterAI
{
    private const float FIND_RANGE = 35f;
    private const float TIME_BETWEEN_SEARCHING = 3f;

    private DateTime _nextSearchingTime = DateTime.UtcNow;
    private NetPlayer _targetPlayer;

    private JumperMovement _movement;
    private MonsterCombat _combat;

	void Start() 
    {
        _movement = GetComponent<JumperMovement>();
        _combat = GetComponent<MonsterCombat>();

        InitializeMonster(); 
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

            if (_targetPlayer != null)
            {
                ChaseAndAttackPlayer();

                _movement.RotateToTarget(_targetPlayer.transform.position);
            }         
        }
	}

    void OnDead()
    {
        enabled = false; 
    }

    private void FindTargetPlayer()
    {
        _targetPlayer = FindTargetPlayer(FIND_RANGE);
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
        if (_combat.IsEnoughTimeToAttack() && !IsTargetBehindObstacle(_targetPlayer.transform))
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
}

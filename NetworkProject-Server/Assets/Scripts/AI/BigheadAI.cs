using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToClient;

public class BigheadAI : MonsterAI
{
    private const float FIND_RANGE = 35f;
    private const float TIME_BETWEEN_SEARCHING = 3f;

    private DateTime _nextSearchingTime = DateTime.UtcNow;
    private NetPlayer _targetPlayer;

    private MonsterMovement _movement;
    private MonsterCombat _combat;

    void Start()
    {
        _movement = GetComponent<MonsterMovement>();
        _movement.MovementType = 0;
        _combat = GetComponent<MonsterCombat>();

        InitializeMonster();
    }

    void FixedUpdate()
    {
        rigidbody.angularVelocity = Vector3.zero;

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
        if (_movement.MovementType != 1)
        {
            _movement.MovementType = 1;
            SendUpdateSpeedToChase();
        }

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

    private void SendUpdateSpeedToWalk()
    {
        GetComponent<NetMonster>().SendChangeMovementType(0);
    }

    private void SendUpdateSpeedToChase()
    {
        GetComponent<NetMonster>().SendChangeMovementType(1);
    }
}

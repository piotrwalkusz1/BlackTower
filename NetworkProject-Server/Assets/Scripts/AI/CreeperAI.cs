using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Monsters;

public class CreeperAI : MonsterAI
{
    private const float TIME_BETWEEN_SEARCHING = 2f;

    private DateTime _nextSearchingTime = DateTime.UtcNow;
    private NetPlayer _targetPlayer;

    private MonsterCombat _combat;

    void Start()
    {
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
                RotateToPlayer();

                TryAttack();
            }
        }
    }

    void OnDead()
    {
        enabled = false;
    }

    private void FindTargetPlayer()
    {
        _targetPlayer = FindTargetPlayer(_combat.AttackRange * 5);
    }

    private void TryAttack()
    {
        if (_combat.IsEnoughTimeToAttack() && _combat.IsEnoughDistanceToAttack(_targetPlayer.transform.position) && !IsTargetBehindObstacle(_targetPlayer.transform))
        {
            _combat.Attack(_targetPlayer.GetComponent<HealthSystem>());
        }
    }

    private void RotateToPlayer()
    {
        Vector3 newDir = _targetPlayer.transform.position - transform.position;

        newDir.y = 0;

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

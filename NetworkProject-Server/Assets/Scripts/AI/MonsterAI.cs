using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Monsters;

public abstract class MonsterAI : MonoBehaviour
{
    protected void InitializeMonster()
    {
        MonsterFullData monster = (MonsterFullData)MonsterRepository.GetMonster(GetComponent<NetMonster>().IdMonster);

        var stats = GetComponent<MonsterStats>();
        int hp = stats.HP;
        monster.Stats.CopyToStats(stats);
        stats.HP = hp;

        InitializeItemsToDrop(monster);
    }

    protected NetPlayer FindTargetPlayer(float range)
    {
        NetPlayer[] findPlayers = GameObject.FindObjectsOfType(typeof(NetPlayer)) as NetPlayer[];

        List<NetPlayer> players = new List<NetPlayer>(findPlayers);

        while (players.Count > 0)
        {
            float nearest = range;
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

            if (nearestPlayer == null)
            {
                return null;
            }

            if (IsTargetBehindObstacle(nearestPlayer.transform))
            {
                players.Remove(nearestPlayer);

                continue;
            }
            else
            {
                return nearestPlayer;
            }
        }

        return null;
    }

    protected bool IsTargetBehindObstacle(Transform target)
    {
        RaycastHit hitInfo;
        Vector3 dir = target.position - transform.position;

        if (Physics.Raycast(transform.position, dir, out hitInfo))
        {
            return hitInfo.transform != target;
        }
        else
        {
            MonoBehaviour.print("Błąd. W nic nie trafiono.");

            return true;
        }
    }

    private void InitializeItemsToDrop(MonsterFullData monster)
    {
        Drop drop = GetComponent<Drop>();

        drop.AddItemToDrop(monster.Drop.ToArray());
    }
}

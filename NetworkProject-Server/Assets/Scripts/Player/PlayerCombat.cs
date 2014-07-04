using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;

[System.CLSCompliant(false)]
public class PlayerCombat : MonoBehaviour
{
    public float _attackSpeed;
    public int _minDmg;
    public int _maxDmg;
    public Transform _bulletRespawn;

    private NetPlayer _netPlayer;

    private const float _speed = 10f;
    private const float _liveTime = 5f;

    void Awake()
    {
        _netPlayer = GetComponent<NetPlayer>();
    }
    
    public void Attack(Vector3 direction)
    {
        if (!WeaponIsEquiped())
        {
            return;
        }

        Attacker attacker = new Attacker(gameObject);
        int dmg = UnityEngine.Random.Range(_minDmg, _maxDmg);
        AttackInfo attackInfo = new AttackInfo(attacker, dmg, DamageType.Physical);

        BulletInfo bulletInfo = new BulletInfo();
        bulletInfo._attackInfo = attackInfo;
        bulletInfo._direction = direction;
        bulletInfo._liveTime = _liveTime;
        bulletInfo._position = _bulletRespawn.position;
        bulletInfo._speed = _speed;
        bulletInfo._shooterId = GetComponent<NetObject>().IdNet;

        SceneBuilder.CreateBullet(bulletInfo, gameObject);

        _netPlayer.SendAttackMessage();
    }

    private bool WeaponIsEquiped()
    {
        return !GetComponent<PlayerEquipment>().IsEmptySlot(BodyPartType.RightHand);
    }
}

﻿using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Combat;
using NetworkProject.Connection.ToServer;

[System.CLSCompliant(false)]
public class PlayerCombat : Combat
{
    public int AttackSpeed { get; set; }
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }  
    public float BulletSpeed { get; set; }
    public float BulletLifeTime { get; set; }

    public Transform _bulletRespawn;

    private Func<Bullet> _createBulletFunction;

    void Awake()
    {
        _createBulletFunction = delegate()
        {
            return new NormalBullet(BulletSpeed);
        };

        BulletLifeTime = 5f;
        BulletSpeed = 5f;
    }
    
    public void Attack(AttackToServer attack)
    {
        if (WeaponIsEquiped())
        {
            Attacker attacker = new Attacker(gameObject);
            int dmg = UnityEngine.Random.Range(MinDmg, MaxDmg);
            AttackInfo attackInfo = new AttackInfo(attacker, dmg, DamageType.Physical);

            BulletInfo bulletInfo = new BulletInfo();
            bulletInfo.Bullet = _createBulletFunction();
            bulletInfo.AttackInfo = attackInfo;
            bulletInfo.LiveTime = BulletLifeTime;
            bulletInfo.Position = _bulletRespawn.position;
            bulletInfo.Rotation = Quaternion.LookRotation(attack.Direction);

            SceneBuilder.CreateBullet(bulletInfo, gameObject);

            GetComponent<NetPlayer>().SendAttackMessage();
        }
    }

    private bool WeaponIsEquiped()
    {
        return GetComponent<PlayerEquipment>().IsEquipedWeapon();
    }

}

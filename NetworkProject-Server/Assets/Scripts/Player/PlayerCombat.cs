using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Combat;
using NetworkProject.Connection.ToServer;

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

        BulletLifeTime = 3f;
        BulletSpeed = 10f;
    }
    
    public void Attack(AttackToServer attack)
    {
        if (IsWeaponEquiped())
        {
            Attacker attacker = new Attacker(gameObject);
            int dmg = UnityEngine.Random.Range(MinDmg, MaxDmg);
            AttackInfo attackInfo = new AttackInfo(attacker, dmg, DamageType.Physical);

            BulletInfo bulletInfo = new BulletInfo(
                bullet: _createBulletFunction(),
                liveTime: BulletLifeTime,
                position: GetCollisionPointToRespawn(),
                rotation: Quaternion.LookRotation(attack.Direction),
                attackInfo: attackInfo);

            SceneBuilder.CreateBullet(bulletInfo, gameObject);

            GetComponent<NetPlayer>().SendAttackMessage();
        }
    }

    private bool IsWeaponEquiped()
    {
        return GetComponent<PlayerEquipment>().IsEquipedWeapon();
    }

    private Vector3 GetCollisionPointToRespawn()
    {
        RaycastHit hitInfo;
        Vector3 dir = _bulletRespawn.transform.position - transform.position;

        if (Physics.Raycast(transform.position, dir, out hitInfo, dir.magnitude))
        {
            return hitInfo.point;
        }
        else
        {
            return _bulletRespawn.transform.position;
        }
    }
}

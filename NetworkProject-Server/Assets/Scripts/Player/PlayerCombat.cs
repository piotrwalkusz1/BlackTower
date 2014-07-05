using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Combat;

[System.CLSCompliant(false)]
public class PlayerCombat : MonoBehaviour
{
    public float AttackSpeed { get; set; }
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }
    public Transform BulletRespawn { get; set; }
    public float BulletSpeed { get; set; }
    public float BulletLifeTime { get; set; }

    private Func<Bullet> _createBulletFunction;

    void Awake()
    {
        _createBulletFunction = delegate()
        {
            return new NormalBullet(BulletSpeed);
        };
    }
    
    public void Attack(Vector3 direction)
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
            bulletInfo.Position = BulletRespawn.position;
            bulletInfo.Rotation = Quaternion.LookRotation(direction);

            SceneBuilder.CreateBullet(bulletInfo, gameObject);

            GetComponent<NetPlayer>().SendAttackMessage();
        }
    }

    private bool WeaponIsEquiped()
    {
        return !GetComponent<PlayerEquipment>().IsEmptySlot(BodyPartSlot.RightHand);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Spells;
using NetworkProject.Buffs;

public static partial class SpellActionsRepository
{
    private static List<SpellFunction> _spells = new List<SpellFunction>();

    static SpellActionsRepository()
    {
        _spells.Add(TransformToFire_0);
        _spells.Add(Teleport_1);
        _spells.Add(IceWall_2);
        _spells.Add(Bomb_3);
    }

    public static SpellFunction GetSpellAction(int idSpell)
    {
        return _spells[idSpell];
    }

    public static void TransformToFire_0(SpellCaster caster, int lvlSpell, params ISpellCastOption[] options)
    {
        DateTime endTimeBuff = DateTime.UtcNow.AddSeconds(7);

        caster.Buffs.AddBuff(new Buff(0, 0, endTimeBuff));
    }

    public static void Teleport_1(SpellCaster caster, int lvlSpell, params ISpellCastOption[] options)
    {
        float teleportDistance = 15f;
        var targetPosition = options.First(x => x is SpellCastOptionTargetPosition) as SpellCastOptionTargetPosition;

        GameObject flash1 = SceneBuilder.CreateVisualObject(0, caster.transform.position, caster.transform.eulerAngles.y);
        GameObject.Destroy(flash1, 0.5f);

        Vector3 dir = (targetPosition.Position - caster.transform.position).normalized;

        caster.GetComponent<CharacterController>().Move(dir * teleportDistance);

        Vector3 newPosition = caster.transform.position;

        GameObject flash2 = SceneBuilder.CreateVisualObject(0, newPosition, caster.transform.eulerAngles.y);
        GameObject.Destroy(flash2, 0.5f);

        caster.GetComponent<PlayerMovement>().SendUpdatePositionToOwnerAndWaitForResponse();
        caster.GetComponent<NetObject>().SendChangePosition();
    }

    private static void IceWall_2(SpellCaster caster, int lvlSpell, params ISpellCastOption[] options)
    {
        float maxDistance = 15f;
        float lifeTime = 10f;
        var targetPosition = options.First(x => x is SpellCastOptionTargetPosition) as SpellCastOptionTargetPosition;
        Vector3 dir = targetPosition.Position - caster.transform.position;

        if (dir.sqrMagnitude > maxDistance * maxDistance)
        {
            return;
        }

        RaycastHit hitInfo;

        if(Physics.Raycast(caster.transform.position, dir, out hitInfo, maxDistance))
        {
            GameObject prefab = StaticRepository.Prefabs._objects[0];
            GameObject ice = SceneBuilder.CreateVisualObject(prefab, hitInfo.point, 0f);

            Vector3 offset = caster.transform.TransformDirection(Vector3.right);

            GameObject ice2 = SceneBuilder.CreateVisualObject(prefab, hitInfo.point + offset, 0f);
            GameObject ice3 = SceneBuilder.CreateVisualObject(prefab, hitInfo.point - offset, 0f);

            HealthSystem hp = ice.GetComponent<HealthSystem>();
            hp.ChangeMaxHP(100);
            hp.ChangeHp(100);

            hp = ice2.GetComponent<HealthSystem>();
            hp.ChangeMaxHP(100);
            hp.ChangeHp(100);

            hp = ice3.GetComponent<HealthSystem>();
            hp.ChangeMaxHP(100);
            hp.ChangeHp(100);

            MonoBehaviour.Destroy(ice, lifeTime);
            MonoBehaviour.Destroy(ice2, lifeTime);
            MonoBehaviour.Destroy(ice3, lifeTime);
        }
    }

    private static void Bomb_3(SpellCaster caster, int lvlSpell, params ISpellCastOption[] options)
    {
        float lifeTime = 10f;

        Vector3 offset = caster.transform.TransformDirection(0, 3, 3);

        GameObject bomb = SceneBuilder.CreateVisualObject(StaticRepository.Prefabs._objects[1], caster.transform.position + offset, caster.transform.eulerAngles.y);

        Attacker attacker = new Attacker(caster.gameObject);
        AttackInfo attackInfo = new AttackInfo(attacker, 200, DamageType.Mental);

        ExplosionDamage explosion = bomb.GetComponent<ExplosionDamage>();
        explosion.AttackInfo = attackInfo;
        explosion.EndTime = DateTime.UtcNow.AddSeconds(lifeTime);

        NetPlayer netPlayer = caster.GetComponent<NetPlayer>();

        NetControlled netControlled = bomb.GetComponent<NetControlled>();
        netControlled.ControllerAddress = netPlayer.OwnerAddress;
        netControlled.SendCreateAndTakeOverToOwner();
    }
}

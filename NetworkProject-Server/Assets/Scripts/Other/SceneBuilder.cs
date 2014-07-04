using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public static class SceneBuilder
{
    private static int _nextIdNet = 0;

    public static GameObject CreatePlayer(RegisterCharacter characterData, IConnectionMember address)
    {
        Vector3 position = characterData.EndPosition;
        GameObject playerInstantiate = GameObject.Instantiate(StaticRepository.Prefabs._player, position, Quaternion.Euler(Vector3.zero)) as GameObject;

        NetPlayer netPlayer = playerInstantiate.GetComponent<NetPlayer>();
        netPlayer.InitializePlayer(GetNextNetId(), characterData, address);

        return playerInstantiate;
    }

    public static GameObject CreateBullet(BulletInfo bulletInfo, GameObject attacker)
    {
        GameObject bullet = GameObject.Instantiate(StaticRepository.Prefabs._bullet, bulletInfo._position, Quaternion.LookRotation(bulletInfo._direction)) as GameObject;

        Damage damage = bullet.GetComponent<Damage>();
        damage._attackInfo = bulletInfo._attackInfo;
        damage._attacker = attacker;
        

        bullet.rigidbody.AddRelativeForce(Vector3.forward * bulletInfo._speed, ForceMode.VelocityChange);

        NetBullet netBullet = bullet.GetComponent<NetBullet>();
        netBullet.IdNet = _nextIdNet;
        _nextIdNet++;
        netBullet._speed = bulletInfo._speed;

        GameObject.Destroy(bullet, bulletInfo._liveTime);

        return bullet;
    }

    public static GameObject CreateMonster(MonsterType monsterType, Vector3 position, float rotation)
    {
        GameObject monster = StaticRepository.Prefabs.GetMonster(monsterType);

        monster = GameObject.Instantiate(monster, position, Quaternion.Euler(0, rotation, 0)) as GameObject;

        MonsterInfo monsterInfo = MonsterRepository.GetMonsterInfo(monsterType);

        NetMonster netMonster = monster.GetComponent<NetMonster>();
        netMonster.IdNet = GetNextNetId();

        MonsterStats stats = monster.GetComponent<MonsterStats>();
        stats.MaxHp = monsterInfo._maxHp;
        stats.HP = monsterInfo._maxHp;
        stats.MovingSpeed = monsterInfo._movingSpeed;

        return monster;
    }

    public static GameObject CreateItem(int idItem, Vector3 position)
    {
        GameObject item = GameObject.Instantiate(StaticRepository.Prefabs._item, position, Quaternion.Euler(Vector3.zero)) as GameObject;

        NetItem netItem = item.GetComponent<NetItem>();
        netItem.IdNet = _nextIdNet;
        _nextIdNet++;
        netItem.Item = new Item(idItem);

        return item;
    }

    public static GameObject CreateVisualObject(VisualObjectType visualObject, Vector3 position, float rotation)
    {
        GameObject instantiate = GameObject.Instantiate(StaticRepository.Prefabs._visualObject, position, Quaternion.Euler(0, rotation, 0)) as GameObject;

        instantiate.GetComponent<NetVisualObject>()._modelType = visualObject;

        return instantiate;
    }

    public static void RespawnPlayer(NetPlayer respawnedPlayer, PlayerRespawn respawn)
    {
        respawnedPlayer.transform.position = respawn.transform.position;

        Server.SendMessageYourRespawn(respawn.transform.position, respawnedPlayer.Address);

        respawnedPlayer.GetComponent<PlayerHealthSystem>().RecuperateAndSendHpUpdating();
    }

    public static void DeletePlayer(OnlineCharacter onlineCharacter)
    {
        GameObject.Destroy(onlineCharacter.GameObject);
    }

    public static void DeleteObject(GameObject gameObject)
    {
        MonoBehaviour.Destroy(gameObject);
    }

    public static int GetNextNetId()
    {
        return _nextIdNet++;
    }
}

using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Combat;
using NetworkProject.Monsters;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public static class SceneBuilder
{
    private static int _nextIdNet = 0;

    public static GameObject CreatePlayer(RegisterCharacter characterData, IConnectionMember address)
    {
        Vector3 position = characterData.EndPosition;
        GameObject playerInstantiate = GameObject.Instantiate(StaticRepository.Prefabs._player, position, Quaternion.Euler(Vector3.zero)) as GameObject;

        NetPlayer player = playerInstantiate.GetComponent<NetPlayer>();
        player.Initialize(GetNextIdNet(), characterData, address);

        return playerInstantiate;
    }

    public static GameObject CreateBullet(BulletInfo bulletInfo, GameObject attacker)
    {
        GameObject instantiate = bulletInfo.Bullet.CreateInstantiate(bulletInfo.Position, bulletInfo.Rotation);

        DamageBullet damage = instantiate.GetComponent<DamageBullet>();
        damage._attackInfo = bulletInfo.AttackInfo;

        NetBullet netBullet = instantiate.GetComponent<NetBullet>();
        netBullet.IdNet = GetNextIdNet();
        netBullet.Bullet = bulletInfo.Bullet;

        GameObject.Destroy(instantiate, bulletInfo.LiveTime);

        return instantiate;
    }

    public static GameObject CreateMonster(int idMonster, Vector3 position, float rotation)
    {
        GameObject instantiate = StaticRepository.Prefabs.GetMonster(idMonster);

        instantiate = GameObject.Instantiate(instantiate, position, Quaternion.Euler(0, rotation, 0)) as GameObject;

        MonsterFullData monsterInfo = (MonsterFullData)MonsterRepository.GetMonster(idMonster);

        NetMonster netMonster = instantiate.GetComponent<NetMonster>();
        netMonster.IdNet = GetNextIdNet();

        MonsterStats stats = instantiate.GetComponent<MonsterStats>();

        monsterInfo.Stats.CopyToStats(stats);

        stats.HP = stats.MaxHP;

        return instantiate;
    }

    public static GameObject CreateItem(int idItem, Vector3 position)
    {
        GameObject item = GameObject.Instantiate(StaticRepository.Prefabs._item, position, Quaternion.Euler(Vector3.zero)) as GameObject;

        NetItem netItem = item.GetComponent<NetItem>();
        netItem.IdNet = GetNextIdNet();
        netItem.Item = new Item(idItem);

        return item;
    }

    public static GameObject CreateVisualObject(int idVisualObject, Vector3 position, float rotation)
    {
        GameObject instantiate = GameObject.Instantiate(StaticRepository.Prefabs._visualObject, position, Quaternion.Euler(0, rotation, 0)) as GameObject;

        NetVisualObject netVisualObject = instantiate.GetComponent<NetVisualObject>();
        netVisualObject.IdNet = GetNextIdNet();
        netVisualObject.IdVisualObject = idVisualObject;       

        return instantiate;
    }

    public static void RespawnPlayer(NetPlayer respawnedPlayer, PlayerRespawn respawn)
    {
        respawnedPlayer.transform.position = respawn.transform.position;

        var request = new RespawnToClient(respawnedPlayer.IdNet, respawn.transform.position);

        respawnedPlayer.GetComponent<PlayerHealthSystem>().RecuperateAndSendHPUpdate();


        NetPlayer netPlayer = respawnedPlayer.GetComponent<NetPlayer>();

        netPlayer.SendRespawnMessage();

        netPlayer.SendRespawnMessageToOwner();
    }

    public static void DeletePlayer(OnlineCharacter onlineCharacter)
    {
        GameObject.Destroy(onlineCharacter.Instantiate);
    }

    public static void DeleteObject(GameObject gameObject)
    {
        MonoBehaviour.Destroy(gameObject);
    }

    public static int GetNextIdNet()
    {
        return _nextIdNet++;
    }
}

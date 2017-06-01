using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Items;
using Standard;

public static class SceneBuilder
{
    private static IGameObjectRepository _gameObjectRepository;

    private static bool _newSceneIsLoading;

    static SceneBuilder()
    {
        Standard.Updating.LevelWasLoadedEvent += OnLevelWasLoaded;

        _gameObjectRepository = Standard.IoC.GetGameObjectRepsitory();
    }

    public static void CreateScene(GoIntoWorldToClient worldInfo)
    {
        Application.LoadLevel("Map" + worldInfo.MapNumber.ToString());

        _newSceneIsLoading = true;

        GUIController.ShowPlayerInterface();

        Client.ResetNetOnwPlayer();
    }

    public static bool SceneIsLoadind()
    {
        return _newSceneIsLoading;
    }

    public static GameObject CreateOwnPlayer(CreateOwnPlayerToClient playerInfo)
    {
        GameObject player = GameObject.Instantiate(Prefabs.PlayerOwner, playerInfo.Position, Quaternion.Euler(Vector3.zero)) as GameObject;

        Client.SetNetOwnPlayer(player.GetComponent<NetOwnPlayer>());

        var netOwnPlayer = player.GetComponent<NetOwnPlayer>();
        netOwnPlayer.InitializePlayer(playerInfo);

        player.GetComponent<PlayerGeneratorModel>().CreateModel();

        ChangeCameraToPlayer(player);

        player.GetComponent<TakeOver>().Activate();

        if (player.GetComponent<PlayerHealth>().HP == 0)
        {
            GUIController.ShowDeadMessage();
        }

        return player;
    }

    public static void CreateOtherPlayer(CreateOtherPlayerToClient otherPlayer)
    {
        GameObject player = GameObject.Instantiate(Prefabs.PlayerOther, otherPlayer.Position, Quaternion.Euler(0, otherPlayer.Rotation, 0)) as GameObject;
        NetOtherPlayer netOtherPlayer = player.GetComponent<NetOtherPlayer>();

        netOtherPlayer.InitializePlayer(otherPlayer);

        player.GetComponent<PlayerGeneratorModel>().CreateModel();
    }

    public static void CreateBullet(CreateBulletToClient bullet)
    {
        GameObject bulletGameObject = bullet.Bullet.CreateInstantiate(bullet.Position, Quaternion.Euler(bullet.FullRotation));

        var netBullet = bulletGameObject.GetComponent<NetBullet>();
        netBullet.IdNet = bullet.IdNet;
        netBullet.Bullet = bullet.Bullet;
    }

    public static void CreateMonster(CreateMonsterToClient monster)
    {
        GameObject monsterGO = GameObject.Instantiate(Prefabs.GetMonsterModel(monster.IdMonster), monster.Position,
            Quaternion.Euler(0, monster.Rotation, 0)) as GameObject;

        monsterGO.GetComponent<NetObject>().IdNet = monster.IdNet;

        MonsterStats stats = monsterGO.GetComponent<MonsterStats>();
        monster.MonsterStats.CopyToStats(stats);
    }

    public static void CreateItem(CreateItemToClient item)
    {
        GameObject prefab = Prefabs.GetItemByIdItem(item.IdItem);

        GameObject itemGO = GameObject.Instantiate(prefab, item.Position, Quaternion.identity) as GameObject;

        NetItem netItem = itemGO.GetComponent<NetItem>();
        netItem.IdNet = item.IdNet;
        netItem.IdItem = item.IdItem;
    }

    public static void CreateVisualObject(CreateVisualObjectToClient visualObject)
    {
        GameObject instantiate = GameObject.Instantiate(Prefabs.GetVisualObject(visualObject.IdVisualObject),
            visualObject.Position, Quaternion.Euler(0, visualObject.Rotation, 0)) as GameObject;

        foreach (var netObject in instantiate.GetComponents<NetObject>())
        {
            netObject.IdNet = visualObject.IdNet;
        }
    }

    public static void CreateNPC(CreateNPCToClient npc)
    {
        var npcGO = GameObject.Instantiate(Prefabs.GetNPCModel(npc.ModelId), npc.Position,
            Quaternion.Euler(0, npc.Rotation, 0)) as GameObject;

        var netNPC = npcGO.GetComponent<NetNPC>();
        netNPC.IdNet = npc.IdNet;
        netNPC.IdNPC = npc.IdNPC;
        netNPC.IdModel = npc.ModelId;

        NPCRepository.InitalizeNPC(npcGO, npc.IdNPC);
    }

    public static NetObject GetNetObjectByIdNet(int idNet)
    {
        return _gameObjectRepository.GetNetObjects().FirstOrDefault(x => x.IdNet == idNet);
    }

    public static NetPlayer GetNetPlayerByIdNet(int idNet)
    {
        return _gameObjectRepository.GetNetPlayers().FirstOrDefault(x => x.IdNet == idNet);
    }

    public static void DeleteObject(GameObject objectToDelete)
    {
        _gameObjectRepository.Delete(objectToDelete);

        GameObject.Destroy(objectToDelete);
    }

    private static void ChangeCameraToPlayer(GameObject player)
    {
        Camera.main.gameObject.SetActive(false);

        PlayerCamera playerCamera = player.GetComponent<PlayerCamera>();
        playerCamera._camera.SetActive(true);
    }

    private static void OnLevelWasLoaded(int lvl)
    {
        _newSceneIsLoading = false;
    }
}

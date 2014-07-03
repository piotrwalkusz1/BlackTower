using UnityEngine;
using System.Collections;
using NetworkProject;
using Standard;

[System.CLSCompliant(false)]
public static class SceneBuilder
{
    private static bool _newSceneIsLoading;

    static SceneBuilder()
    {
        Standard.Updating.LevelWasLoadedEvent += OnLevelWasLoaded;
    }

    public static void CreateScene(WorldInfoPackage worldInfo)
    {
        Application.LoadLevel("Map" + worldInfo.MapNumber.ToString());

        _newSceneIsLoading = true;
    }

    public static bool SceneIsLoadind()
    {
        return _newSceneIsLoading;
    }

    public static void CreateOwnPlayer(OwnPlayerPackage playerInfo)
    {
        GameObject player = GameObject.Instantiate(Prefabs.PlayerOwner, playerInfo.Position, Quaternion.Euler(Vector3.zero)) as GameObject;
        var netOwnPlayer = player.GetComponent<NetOwnPlayer>();
        netOwnPlayer.InitializePlayer(playerInfo);

        player.GetComponent<PlayerGeneratorModel>().CreateModel();

        ChangeCameraToPlayer(player);

        Client.SetNetOwnPlayer(netOwnPlayer);
    }

    public static void CreateOtherPlayer(OtherPlayerPackage otherPlayer)
    {
        GameObject player = GameObject.Instantiate(Prefabs.PlayerOther, otherPlayer.Position, Quaternion.Euler(0, otherPlayer.Rotation, 0)) as GameObject;
        NetOtherPlayer netOtherPlayer = player.GetComponent<NetOtherPlayer>();

        netOtherPlayer.InitializePlayer(otherPlayer);

        player.GetComponent<PlayerGeneratorModel>().CreateModel();
    }

    public static void CreateBullet(BulletPackage bullet)
    {
        GameObject bulletGameObject = GameObject.Instantiate(Prefabs.Bullet, bullet._position, Quaternion.LookRotation(bullet._direction)) as GameObject;

        bulletGameObject.rigidbody.AddRelativeForce(Vector3.forward * bullet._speed, ForceMode.VelocityChange);

        bulletGameObject.GetComponent<NetObject>().IdObject = bullet.IdObject;
    }

    public static void CreateMonster(MonsterPackage monster)
    {
        GameObject monsterGO = GameObject.Instantiate(Prefabs.GetMonsterModel(monster._monsterType), monster._position, Quaternion.Euler(0, monster._rotation, 0)) as GameObject;

        monsterGO.GetComponent<NetObject>().IdObject = monster.IdObject;

        MonsterStats stats = monsterGO.GetComponent<MonsterStats>();
        stats.Hp = monster._stats._hp;
        stats.MaxHp = monster._stats._maxHp;
        stats.MovingSpeed = monster._stats._movementSpeed;
    }

    public static void CreateItem(ItemPackage item)
    {
        GameObject prefab = ChooseItem(item.IdItem);

        GameObject itemGO = GameObject.Instantiate(prefab, item.Position, Quaternion.identity) as GameObject;

        NetItem netItem = itemGO.GetComponent<NetItem>();
        netItem.IdObject = item.IdObject;
        netItem.IdItem = item.IdItem;
    }

    public static void CreateVisualObject(VisualObjectPackage visualObject)
    {
        GameObject instantiate = GameObject.Instantiate(Prefabs.GetVisualObject(visualObject._objectType),
            visualObject._position, Quaternion.Euler(0, visualObject._rotation, 0)) as GameObject;

        instantiate.GetComponent<NetObject>().IdObject = visualObject.IdObject;
    }

    private static GameObject ChooseItem(int idItem)
    {
        ItemData item = ItemBase.GetAnyItem(idItem);

        return Prefabs.Items[item._idPrefabOnScene];
    }

    private static void ChangeCameraToPlayer(GameObject player)
    {
        Camera.main.gameObject.SetActive(false);

        PlayerCamera playerCamera = player.GetComponent<PlayerCamera>();
        playerCamera._camera.SetActive(true);
    }

    private static void OnLevelWasLoaded(int i)
    {
        _newSceneIsLoading = false;
    }
}

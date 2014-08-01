using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;

public class Prefabs : MonoBehaviour
{
    public GameObject _guiEquipment;
    public GameObject _guiItemInEquipment;
    public GameObject _guiCharacter;
    
    public GameObject _playerOwner;
    public GameObject _playerOther;
    public List<GameObject> _playerModels;
    public List<GameObject> _monsters;
    public List<GameObject> _items;
    public List<GameObject> _visualObjects;

    public static GameObject GUIEquipment
    {
        get
        {
            return _prefabs._guiEquipment;
        }
        set
        {
            _prefabs._guiEquipment = value;
        }
    }
    public static GameObject GUIItemInEquipment
    {
        get
        {
            return _prefabs._guiItemInEquipment;
        }
        set
        {
            _prefabs._guiItemInEquipment = value;
        }
    }
    public static GameObject GUICharacter
    {
        get
        {
            return _prefabs._guiCharacter;
        }
        set
        {
            _prefabs._guiCharacter = value;
        }
    }
    public static List<GameObject> PlayerModels
    {
        get { return _prefabs._playerModels; }
        set { _prefabs._playerModels = value; }
    }
    public static GameObject PlayerOwner
    {
        get
        {
            return _prefabs._playerOwner;
        }
    }
    public static GameObject PlayerOther
    {
        get
        {
            return _prefabs._playerOther;
        }
    }
    public static List<GameObject> Monsters
    {
        get
        {
            return _prefabs._monsters;
        }
        set
        {
            _prefabs._monsters = value;
        }
    }
    public static List<GameObject> Items
    {
        get
        {
            return _prefabs._items;
        }
        set
        {
            _prefabs._items = value;
        }
    }
    public static List<GameObject> VisualObjects
    {
        get
        {
            return _prefabs._visualObjects;
        }
        set
        {
            _prefabs._visualObjects = value;
        }
    }

    private static Prefabs _prefabs;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _prefabs = this;
    }

    public static GameObject GetItemByIdItem(int idItem)
    {
        VisualItemData item = (VisualItemData)ItemRepository.GetItemByIdItem(idItem);

        return GetItemByIdPrefab(item.IdPrefabOnScene);
    }

    public static GameObject GetItemByIdPrefab(int idPrefab)
    {
        return Items[idPrefab];
    }

    public static GameObject GetPlayerModelByBreed(int breed)
    {
        return _prefabs._playerModels[breed];
    }

    public static GameObject GetMonsterModel(int id)
    {
        return Monsters[id];
    }

    public static GameObject GetVisualObject(int id)
    {
        return _prefabs._visualObjects[id];
    }
}

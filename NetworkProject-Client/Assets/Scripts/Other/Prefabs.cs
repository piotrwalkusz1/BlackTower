using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
public class Prefabs : MonoBehaviour
{
    public GameObject _guiEquipment;
    public GameObject _guiItemInEquipment;
    public GameObject _guiCharacter;
    public GameObject _playerOwner;
    public GameObject _playerOther;
    public GameObject _bullet;
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
    public static GameObject Bullet
    {
        get
        {
            return _prefabs._bullet;
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

    public static GameObject GetMonsterModel(int id)
    {
        return Monsters[id];
    }

    public static GameObject GetVisualObject(int id)
    {
        return _prefabs._visualObjects[id];
    }
}

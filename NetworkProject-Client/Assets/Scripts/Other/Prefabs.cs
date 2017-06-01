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
    public GameObject _guiQuestProposal;
    public GameObject _guiDialog;
    public GameObject _guiQuestManager;
    public GameObject _hpBar;
    public GameObject _mpBar;
    public GameObject _guiDescription;
    public GameObject _guiMagicBook;
    public GameObject _guiSpell;
    public GameObject _guiShop;
    public GameObject _guiItemInShop;
    public GameObject _guiHotkeys;
    
    public GameObject _playerOwner;
    public GameObject _playerOther;
    public GameObject _deathEffect;
    public List<GameObject> _playerModels;
    public List<GameObject> _monsters;
    public List<GameObject> _items;
    public List<GameObject> _visualObjects;
    public List<GameObject> _npcModels;

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
    public static GameObject GUIQuestProposal
    {
        get { return _prefabs._guiQuestProposal; }
        set { _prefabs._guiQuestProposal = value; }
    }
    public static GameObject GUIDialog
    {
        get { return _prefabs._guiDialog; }
        set { _prefabs._guiDialog = value; }
    }
    public static GameObject GUIQuestManager
    {
        get { return _prefabs._guiQuestManager; }
        set { _prefabs._guiQuestManager = value; }
    }
    public static GameObject HPBar
    {
        get { return _prefabs._hpBar; }
        set { _prefabs._hpBar = value; }
    }
    public static GameObject MPBar
    {
        get { return _prefabs._mpBar; }
        set { _prefabs._mpBar = value; }
    }
    public static GameObject GUIDescription
    {
        get { return _prefabs._guiDescription; }
    }
    public static GameObject GUIMagicBook
    {
        get { return _prefabs._guiMagicBook; }
    }
    public static GameObject GUISpell
    {
        get { return _prefabs._guiSpell; }
    }
    public static GameObject GUIShop
    {
        get { return _prefabs._guiShop; }
    }
    public static GameObject GUIItemInShop
    {
        get { return _prefabs._guiItemInShop; }
    }
    public static GameObject GUIHotkeys
    {
        get { return _prefabs._guiHotkeys; }
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
    public static GameObject DeathEffect
    {
        get { return _prefabs._deathEffect; }
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
    public static List<GameObject> NPCModels
    {
        get { return _prefabs._npcModels; }
        set { _prefabs._npcModels = value; }
    }

    private static Prefabs _prefabs;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _prefabs = this;
    }

    public static GameObject GetItemByIdItem(int idItem)
    {
        ItemData item = (ItemData)ItemRepository.GetItemByIdItem(idItem);

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

    public static GameObject GetNPCModel(int id)
    {
        return NPCModels[id];
    }
}

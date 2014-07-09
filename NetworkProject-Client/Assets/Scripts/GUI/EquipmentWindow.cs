using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;

public class 
EquipmentWindow : GUIObject
{  
    public Texture2D _texture;
    public Vector2 _externalBorder;
    public int _internalBorder;
    public int _sizeItemImage;
    public Rect _closeButtonRect;

    private OwnPlayerEquipment _equipment;

    private List<ItemInEquipmentWindow> _items;
    private Vector3 _lastMousePosition;

    public EquipmentWindow()
    {
        _items = new List<ItemInEquipmentWindow>();

        for (int i = 0; i < Settings.widthEquipment * Settings.heightEquipment; i++)
        {
            _items.Add(null);
        }
    }

    void Awake()
    {
        _lastMousePosition = Input.mousePosition;       
    }

    void LateUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 offset = Input.mousePosition - _lastMousePosition;

        transform.position += new Vector3(offset.x / Screen.width, offset.y / Screen.height);

        _lastMousePosition = Input.mousePosition;
    }

    public void Initialize()
    {
        InitializeEquipmentGUI();

        InitializeItemsGUI();
    }

    public void SetEquipment(OwnPlayerEquipment equipment)
    {
        _equipment = equipment;
    }

    public void RefreshEquipment()
    {
        Item[] items = _equipment.GetItemsFromBag();

        for (int i = 0; i < items.Length; i++)
        {
            _items[i].SetTextureByItem(items[i]);
        }
    }

    public Vector2 GetPixelPositionToItem(ItemInEquipmentWindow item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == item)
            {
                int x = i % Settings.widthEquipment;
                int y = Mathf.FloorToInt(i / Settings.widthEquipment);

                Vector2 position = _externalBorder + new Vector2(x, y) * (_internalBorder + _sizeItemImage);
                position = new Vector2(position.x, _texture.height - position.y - _sizeItemImage);

                return position;
            }
        }

        throw new System.Exception("Nie ma takiego itemu w tablicy");
    }

    public Item GetItem(ItemInEquipmentWindow item)
    {
        int slot = GetSlotToItem(item);

        return _equipment.GetItemFromBag(slot);
    }

    public int GetSlotToItem(ItemInEquipmentWindow item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == item)
            {
                return i;
            }
        }

        throw new System.Exception("Nie ma takiego itemu w tablicy");
    }

    public bool IsSlotEmpty(ItemInEquipmentWindow item)
    {
        int slot = GetSlotToItem(item);
        
        return _equipment.IsEmptyBagSlot(slot);
    }

    public OwnPlayerStats GetPlayerStats()
    {
        return _equipment.GetComponent<OwnPlayerStats>();
    }

    private void InitializeEquipmentGUI()
    {
        guiTexture.pixelInset = new Rect(0, 0, _texture.width, _texture.height);
        guiTexture.texture = _texture;
    }

    private void InitializeItemsGUI()
    {
        for (int y = 0; y < Settings.heightEquipment; y++)
        {
            for (int x = 0; x < Settings.widthEquipment; x++)
            {
                int index = x + y * Settings.widthEquipment;

                GameObject go = Instantiate(Prefabs.GUIItemInEquipment, Vector3.zero, Quaternion.identity) as GameObject;
                ItemInEquipmentWindow item = go.GetComponent<ItemInEquipmentWindow>();
                item.transform.parent = guiTexture.transform;
                item.transform.localPosition = item.transform.localPosition + new Vector3(0, 0, 0.5f);

                item._equipmentWindow = this;

                item.guiTexture.texture = null;

                Vector2 position = _externalBorder + new Vector2(x, y) * (_internalBorder + _sizeItemImage);
                position = new Vector2(position.x, _texture.height - position.y - _sizeItemImage);

                item.guiTexture.pixelInset = new Rect(position.x, position.y, _sizeItemImage, _sizeItemImage);

                _items[index] = item;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUIShop : GUIObject, IClosable
{
    public int _topMargin;
    public int _leftMargin;
    public int _borderSize;
    public int _imageSize;
    public int _marginBetweenSpells;
    public int _xCount;
    public int _yCount;

    private int _idNetNPC;
    private Shop _shop;
    private Vector3 _lastMousePosition;
    private List<GUIItemInShop> _items;
    private SpellCaster _spellCaster;

    void Awake()
    {
        _lastMousePosition = Input.mousePosition;

        _items = new List<GUIItemInShop>();

        for (int y = 0; y < _yCount; y++)
        {
            for (int x = 0; x < _xCount; x++)
            {
                GameObject go = Instantiate(Prefabs.GUIItemInShop, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                Vector2 position = GetPixelPositionToItem(x, y);
                go.guiTexture.pixelInset = new Rect(position.x, position.y, _imageSize, _imageSize);
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0, transform.localPosition.z + 0.5f);

                var item = go.GetComponent<GUIItemInShop>();
                item.Shop = this;

                _items.Add(item);
            }
        }
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

    public void Refresh(Shop shop, int idNetNPC)
    {
        _idNetNPC = idNetNPC;
        _shop = shop;

        var items = _shop.Items;
        int count = items.Count;

        for (int i = 0; i < _items.Count; i++)
        {
            MonoBehaviour.print(i);

            if (i < count)
            {
                _items[i].ChangeTexture(items[i]);
            }
            else
            {
                _items[i].ChangeTexture(null);
            }
        }
    }

    public Vector2 GetPixelPositionToItem(GUIItemInShop item)
    {
        int index = _items.IndexOf(item);

        Vector2 position = new Vector2(index % _xCount, (int)(index / _yCount));

        return GetPixelPositionToItem((int)position.x, (int)position.y);
    }

    public ItemInShop GetItemInShop(GUIItemInShop item)
    {
        int slot = GetSlot(item);

        if (slot < _shop.Items.Count)
        {
            return _shop.Items[slot];
        }
        else
        {
            return null;
        }
    }

    public int GetSlot(GUIItemInShop item)
    {
        return _items.IndexOf(item);
    }

    public NetNPC GetNPC()
    {
        return GameObject.FindObjectsOfType<NetNPC>().FirstOrDefault(x => x.IdNet == _idNetNPC);
    }

    public void Close()
    {
        GUIController.HideShop();
    }

    public void TrySellItemToPlayer(int slot)
    {
        var netOwnPlayer = Client.GetNetOwnPlayer();

        var eq = netOwnPlayer.GetComponent<OwnPlayerEquipment>();

        _shop.BuyItem(_idNetNPC, eq, slot);
    }

    private Vector2 GetPixelPositionToItem(int x, int y)
    {
        int posX = _leftMargin + (2 * x + 1) * _borderSize + (_imageSize + _marginBetweenSpells) * x;
        int posY = (int)guiTexture.pixelInset.height - _topMargin - (2 * y + 1) * _borderSize - _imageSize * (y + 1) - y * _marginBetweenSpells;

        return new Vector2(posX, posY);
    }
}

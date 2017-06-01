using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUIItemInShop : GUIObject
{
    public GUIShop Shop { get; set; }

    private Vector3 _lastMousePosition;
    private DateTime _lateMouseDown;
    private bool _wasUseItemInLastMouseDown;

    private const float MOUSE_DOWN_INTERVAL_TO_USE = 0.6f;

    protected void Awake()
    {
        _lateMouseDown = new DateTime(0);
        _lastMousePosition = Input.mousePosition;
    }

    protected void LateUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }

    protected void OnMouseEnter()
    {
        ShowDescription();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        GUIController.HideDescription();

        if (_wasUseItemInLastMouseDown)
        {
            _wasUseItemInLastMouseDown = false;
        }
        else
        {
            var interval = DateTime.UtcNow - _lateMouseDown;

            if (interval.TotalSeconds < MOUSE_DOWN_INTERVAL_TO_USE)
            {
                Shop.TrySellItemToPlayer(GetSlot());

                _wasUseItemInLastMouseDown = true;
            }
        }

        _lateMouseDown = DateTime.UtcNow;
    }

    protected void OnMouseExit()
    {
        GUIController.HideDescription();
    } 

    public void ChangeTexture(ItemInShop item)
    {
        if (item == null)
        {
            guiTexture.texture = null;
        }
        else
        {
            guiTexture.texture = ImageRepository.GetImageByItem(item.Item);
        }      
    }

    protected void ShowDescription()
    {
        ItemInShop item = GetItemInShop();

        if (item != null)
        {
            Rect rect = guiTexture.GetScreenRect();

            Vector2 position = new Vector2(rect.x + rect.width, rect.y + rect.height);

            string description = item.GetDescription();

            int sizeItemImage = Shop._imageSize;

            GUIController.ShowDescription(position, description, sizeItemImage, sizeItemImage, Shop);
        }
    }

    protected ItemInShop GetItemInShop()
    {
        return Shop.GetItemInShop(this);
    }

    protected int GetSlot()
    {
        return Shop.GetSlot(this);
    }
}

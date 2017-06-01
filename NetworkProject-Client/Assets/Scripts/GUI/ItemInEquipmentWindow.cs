using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;

public class ItemInEquipmentWindow : GUIObject
{
    public EquipmentWindow _equipmentWindow;

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

    protected void OnMouseUp()
    {
        if (IsEmpty())
        {
            GoToDefaultPlace();
            return;
        }

        GUIElement element = GuiRaycastUnderObject(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (element == null)
        {
            GoToDefaultPlace();
            return;
        }

        GUIObject guiObject = element.GetComponent<GUIObject>();

        if (guiObject == null)
        {
            GoToDefaultPlace();
            return;
        }
        else
        {
            guiObject.OnDropItem(this);
        }
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
                var info = new UseItemInfo(Client.GetNetOwnPlayer().gameObject, GetSlot());

                GetItem().ItemData.UseItem(info);

                _wasUseItemInLastMouseDown = true;
            }
        }

        _lateMouseDown = DateTime.UtcNow;
    }

    protected void OnMouseExit()
    {
        GUIController.HideDescription();
    }

    protected void OnMouseDrag()
    {
        Vector3 offset = Input.mousePosition - _lastMousePosition;

        guiTexture.pixelInset = new Rect(guiTexture.pixelInset.x + offset.x, guiTexture.pixelInset.y + offset.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);

        _lastMousePosition = Input.mousePosition;
    }

    public override void OnDropItem(ItemInEquipmentWindow item)
    {
        if (item._equipmentWindow == _equipmentWindow)
        {
            var request = new ChangeItemsInEquipmentToServer(GetSlot(), item.GetSlot());

            Client.SendRequestAsMessage(request);

            ChangeTextures(guiTexture, item.guiTexture);
        }

        item.GoToDefaultPlace();
    }

    public override void OnDropEquipedItem(ItemInCharacterWindow item)
    {
        if (_equipmentWindow.GetEquipment().TryEquipItemFromBagAndSendMessage(GetSlot(), item.GetSlot()))
        {
            ChangeTextures(guiTexture, item.guiTexture);
        }

        item.GoToDefaultPlace();
    }

    public void GoToDefaultPlace()
    {
        Vector2 position = _equipmentWindow.GetPixelPositionToItem(this);

        guiTexture.pixelInset = new Rect(position.x, position.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
    }


    public Item GetItem()
    {
        return _equipmentWindow.GetItem(this);
    }   

    public bool IsEmpty()
    {
        return _equipmentWindow.IsSlotEmpty(this);
    }

    public int GetSlot()
    {
        return _equipmentWindow.GetSlotToItem(this);
    }

    public void SetTextureByItem(Item item)
    {
        if (item == null)
        {
            guiTexture.texture = null;
        }
        else
        {
            guiTexture.texture = ImageRepository.GetImageByItem(item);
        }
    }

    protected void ShowDescription()
    {
        Item item = GetItem();

        if(item != null)
        {
            Rect rect = guiTexture.GetScreenRect();

            Vector2 position = new Vector2(rect.x + rect.width, rect.y + rect.height);

            string description = item.GetDescription();

            int sizeItemImage = _equipmentWindow._sizeItemImage;

            GUIController.ShowDescription(position, description, sizeItemImage, sizeItemImage, _equipmentWindow);
        }
    }
}

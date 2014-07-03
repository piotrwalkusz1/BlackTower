using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class ItemInEquipmentWindow : GUIObject
{
    public EquipmentWindow _equipmentWindow;

    private Vector3 _lastMousePosition;

    protected void Awake()
    {
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

        GUIElement element = GuiRaycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

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
            Client.SendMessageChangeItemsInEquipment(_equipmentWindow.GetSlotToItem(item), _equipmentWindow.GetSlotToItem(this));

            ChangeTextures(guiTexture, item.guiTexture);
        }

        item.GoToDefaultPlace();
    }

    public override void OnDropEquipedItem(ItemInCharacterWindow item)
    {
        if (IsEmpty() || CanBeEquipedByPlayer(item._itemType))
        {
            ChangeTextures(guiTexture, item.guiTexture);

            Client.SendMessageChangeEquipedItem(GetSlot(), item._itemType);

            item.GoToDefaultPlace();
        }
        else
        {
            item.GoToDefaultPlace();
        }
    }

    public void GoToDefaultPlace()
    {
        Vector2 position = _equipmentWindow.GetPixelPositionToItem(this);

        guiTexture.pixelInset = new Rect(position.x, position.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
    }

    public bool CanBeEquipedByPlayer()
    {
        Item item = GetItem();

        return item.CanBeEquipedByPlayer();
    }

    public bool CanBeEquipedByPlayer(ItemEquipableType type)
    {
        Item item = GetItem();

        return item.CanBeEquipedByPlayer(type);
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
}

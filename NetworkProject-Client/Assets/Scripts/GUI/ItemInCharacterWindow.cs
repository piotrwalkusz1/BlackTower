using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public class ItemInCharacterWindow : GUIObject
{
    public CharacterWindow _characterWindow;

    private Vector3 _lastMousePosition;
    private Vector2 _defaultPosition;

    protected void Awake()
    {
        _lastMousePosition = Input.mousePosition;
        _defaultPosition = new Vector2(guiTexture.pixelInset.x, guiTexture.pixelInset.y);
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
            guiObject.OnDropEquipedItem(this);
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
        if (item.CanBeEquipedByPlayer(_itemType))
        {
            item.GoToDefaultPlace();

            ChangeTextures(guiTexture, item.guiTexture);

            Client.SendMessageChangeEquipedItem(item.GetSlot(), _itemType);
        }
        else
        {
            item.GoToDefaultPlace();
        }
    }

    public override void OnDropEquipedItem(ItemInCharacterWindow item)
    {
        if (item.CanBeEquipedByPlayer(_itemType))
        {
            ChangeTextures(item.guiTexture, guiTexture);

            Client.SendMessageChangeEquipedItems(_itemType, item._itemType);

            item.GoToDefaultPlace();
        }
        else
        {
            item.GoToDefaultPlace();
        }
    }

    public bool CanBeEquipedByPlayer(Item item)
    {
        EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);
        PlayerStats stats = _characterWindow._stats;

        return item.CanEquipe(stats) && DoesBodyPartMatchToItem(itemData);
    }

    public int GetSlot()
    {
        return _characterWindow
    }

    public void GoToDefaultPlace()
    {
        GUITexture gui = GetComponent<GUITexture>();

        gui.pixelInset = new Rect(_defaultPosition.x, _defaultPosition.y, gui.pixelInset.width, gui.pixelInset.height);
    }

    private Vector2 GetDefaultPosition()
    {
        return _defaultPosition;
    }

    private Item GetItem()
    {
        return _characterWindow.PlayerEquipement.GetEquipedItem(_itemType);
    }

    private bool IsEmpty()
    {
        return _characterWindow.PlayerEquipement.IsEmpty(_itemType);
    }

    private bool DoesBodyPartMatchToItem(ItemData itemData)
    {
        return IoC.GetBodyPart(GetSlot()).CanEquipeItemOnThisBodyPart(itemData);
    }
}

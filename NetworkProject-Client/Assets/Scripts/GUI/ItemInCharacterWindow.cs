using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;

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

    public override void OnDropItem(ItemInEquipmentWindow itemWindow)
    {
        if (CanBeEquipedByPlayer(itemWindow.GetItem()))
        {
            var request = new ChangeEquipedItemToServer(itemWindow.GetSlot(), GetSlot());

            Client.SendRequestAsMessage(request);

            ChangeTextures(guiTexture, itemWindow.guiTexture);
        }

        itemWindow.GoToDefaultPlace();
    }

    public override void OnDropEquipedItem(ItemInCharacterWindow itemWindow)
    {
        if (CanBeEquipedByPlayer(itemWindow.GetItem()) && itemWindow.CanBeEquipedByPlayer(GetItem()))
        {
            var request = new ChangeEquipedItemsToServer(itemWindow.GetSlot(), GetSlot());

            Client.SendRequestAsMessage(request);

            ChangeTextures(itemWindow.guiTexture, guiTexture);
        }
        
        itemWindow.GoToDefaultPlace();
    }

    public bool CanBeEquipedByPlayer(Item item)
    {
        var itemData = (IEquipableItemManager)ItemRepository.GetItemByIdItem(item.IdItem);
        PlayerStats stats = _characterWindow.GetPlayerStats();

        return item.CanEquipe(stats) && DoesBodyPartMatchToItem(itemData.GetEquipableItemData());
    }

    public int GetSlot()
    {
        return _characterWindow.GetSlotToItem(this);
    }

    public void GoToDefaultPlace()
    {
        GUITexture gui = GetComponent<GUITexture>();

        gui.pixelInset = new Rect(_defaultPosition.x, _defaultPosition.y, gui.pixelInset.width, gui.pixelInset.height);
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

    private Vector2 GetDefaultPosition()
    {
        return _defaultPosition;
    }

    private Item GetItem()
    {
        return _characterWindow.PlayerEquipement.GetEquipedItem(GetSlot());
    }

    private bool IsEmpty()
    {
        return _characterWindow.PlayerEquipement.IsEmptyEquipedSlot(GetSlot());
    }

    private bool DoesBodyPartMatchToItem(EquipableItemData itemData)
    {
        return IoC.GetBodyPart(GetSlot()).CanEquipeItemOnThisBodyPart(itemData);
    }
}

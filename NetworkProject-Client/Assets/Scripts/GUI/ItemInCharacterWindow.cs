using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;
using NetworkProject.BodyParts;

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

    protected void OnMouseEnter()
    {
        ShowDescription();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        GUIController.HideDescription();
    }

    protected void OnMouseExit()
    {
        GUIController.HideDescription();
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
        if (_characterWindow.GetEquipment().TryEquipItemFromBagAndSendMessage(itemWindow.GetSlot(), GetSlot()))
        {
            ChangeTextures(guiTexture, itemWindow.guiTexture);
        }

        itemWindow.GoToDefaultPlace();
    }

    public override void OnDropEquipedItem(ItemInCharacterWindow itemWindow)
    {
        if (_characterWindow.GetEquipment().TryChangeEquipedItemsAndSendMessage(GetSlot(), itemWindow.GetSlot()))
        {
            ChangeTextures(itemWindow.guiTexture, guiTexture);
        }
        
        itemWindow.GoToDefaultPlace();
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

    public void RefreshTexture()
    {
        Item item = GetItem();

        guiTexture.texture = item == null ? null : ImageRepository.GetImageByItem(item);
    }

    protected void ShowDescription()
    {
        Item item = GetItem();

        if (item != null)
        {
            Rect rect = guiTexture.GetScreenRect();

            Vector2 position = new Vector2(rect.x + rect.width, rect.y + rect.height);

            string description = item.GetDescription();

            GUIController.ShowDescription(position, description, GUIController.SizeItemImage, GUIController.SizeItemImage, _characterWindow);
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
        var bodyPartPackage = IoC.GetBodyPart(GetSlot());

        List<BodyPart> bodyPart = PackageConverter.PackageToBodyPart(new BodyPartPackage[] { bodyPartPackage });

        return bodyPart[0].CanEquipeItemOnThisBodyPart(itemData);
    }
}

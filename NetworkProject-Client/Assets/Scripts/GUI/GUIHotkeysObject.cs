using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUIHotkeysObject : GUIObject
{
    public KeyCode _key;

    public HotkeysObject HotkeysObject { get; set; }

    private Rect _defaultPosition;
    private Vector3 _lastMousePosition;

    protected void Awake()
    {
        _defaultPosition = guiTexture.pixelInset;
        _lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            Use();
        }

        Refresh();
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
        if (HotkeysObject == null || HotkeysObject.IsEmpty())
        {
            GoToDefaultPlace();
            return;
        }

        GUIElement element = GuiRaycastUnderObject(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (element == null)
        {
            GoToDefaultPlace();
            SetHotkeyAndSetUpdate(null);
            return;
        }

        GUIObject guiObject = element.GetComponent<GUIObject>();

        if (guiObject == null)
        {
            GoToDefaultPlace();
            SetHotkeyAndSetUpdate(null);
            return;
        }
        else
        {
            guiObject.OnDropHotkeysObject(this);
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
        if (item.GetItem().ItemData is ItemUsableData)
        {
            SetHotkeyAndSetUpdate(new HotkeysUsableItem(item.GetSlot()));
        }

        item.GoToDefaultPlace();
    }

    public override void OnDropSpell(GUISpell spell)
    {
        SetHotkeyAndSetUpdate(new HotkeysSpell(spell.GetSpell().IdSpell));

        spell.GoToDefaultPlace();
    }

    public override void OnDropHotkeysObject(GUIHotkeysObject hotkeysObject)
    {
        HotkeysObject h = HotkeysObject;
        HotkeysObject = hotkeysObject.HotkeysObject;
        hotkeysObject.HotkeysObject = h;

        hotkeysObject.GoToDefaultPlace();

        GUIHotkeys.SendHotkeysUpdate();
    }

    public void GoToDefaultPlace()
    {
        guiTexture.pixelInset = _defaultPosition;
    }

    public void Use()
    {
        if (HotkeysObject != null)
        {
            HotkeysObject.Use();
        }  
    }

    public void SetHotkeyAndSetUpdate(HotkeysObject hotkey)
    {
        HotkeysObject = hotkey;

        GUIHotkeys.SendHotkeysUpdate();
    }

    public void Refresh()
    {
        if (HotkeysObject == null)
        {
            guiTexture.texture = null;
        }
        else
        {
            guiTexture.texture = HotkeysObject.GetImage();
        }    
    }

    protected void ShowDescription()
    {
        if (HotkeysObject != null)
        {
            Rect rect = guiTexture.GetScreenRect();

            Vector2 position = new Vector2(rect.x + rect.width, rect.y + rect.height);

            string description = HotkeysObject.GetDescription();

            GUIController.ShowDescription(position, description, GUIController.SizeItemImage, GUIController.SizeItemImage, null);
        }
    }
}

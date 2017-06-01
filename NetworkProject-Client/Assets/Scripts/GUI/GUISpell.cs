using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUISpell : GUIObject
{
    public GUIMagicBook _magicBook;

    private Vector3 _lastMousePosition;

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
            guiObject.OnDropSpell(this);
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

        guiTexture.pixelInset = new Rect(guiTexture.pixelInset.x + offset.x, guiTexture.pixelInset.y + offset.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);

        _lastMousePosition = Input.mousePosition;
    }

    public void GoToDefaultPlace()
    {
        Vector2 position = _magicBook.GetPixelPositionToSpell(this);

        guiTexture.pixelInset = new Rect(position.x, position.y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
    }

    public Spell GetSpell()
    {
        return _magicBook.GetSpell(this);
    }

    public void ChangeTexture(Spell spell)
    {
        if (spell == null)
        {
            guiTexture.texture = null;
        }
        else
        {
            guiTexture.texture = ImageRepository.GetImageBySpell(spell);
        }
    }

    private bool IsEmpty()
    {
        return GetSpell() == null;
    }
}

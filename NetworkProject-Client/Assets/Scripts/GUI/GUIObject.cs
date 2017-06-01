using UnityEngine;
using System.Collections;

public class GUIObject : MonoBehaviour
{
    protected virtual void OnMouseDown()
    {
        Focus();

        if (transform.parent != null)
        {
            var guiObject = transform.parent.GetComponent<GUIObject>();

            if (guiObject != null)
            {
                guiObject.Focus();
            }
        }
    }

    public virtual void OnDropItem(ItemInEquipmentWindow item)
    {
        item.GoToDefaultPlace();
    }

    public virtual void OnDropEquipedItem(ItemInCharacterWindow item)
    {
        item.GoToDefaultPlace();
    }

    public virtual void OnDropSpell(GUISpell spell)
    {
        spell.GoToDefaultPlace();
    }

    public virtual void OnDropHotkeysObject(GUIHotkeysObject hotkeysObject)
    {
        hotkeysObject.GoToDefaultPlace();
        hotkeysObject.SetHotkeyAndSetUpdate(null);
    }

    public void Focus()
    {
        float offset = transform.localPosition.z % 1;

        transform.localPosition = new Vector3(transform.position.x, transform.position.y, GUIController.GetStackPosition() + offset);

        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform obj in children)
        {
            if (obj != transform)
            {
                offset = obj.localPosition.z % 1;
                obj.localPosition = new Vector3(obj.position.x, obj.position.y, transform.position.z + offset);
            }
        }
    }

    protected void ChangeTextures(GUITexture gui1, GUITexture gui2)
    {
        Texture tex = gui1.texture;
        gui1.texture = gui2.texture;
        gui2.texture = tex;
    }

    protected GUIElement GuiRaycastUnderObject(Vector2 position)
    {
        Rect firstRect = guiTexture.pixelInset;

        guiTexture.pixelInset = new Rect(100000, 100000, guiTexture.pixelInset.width, guiTexture.pixelInset.height);

        GUILayer layer = Camera.main.GetComponent<GUILayer>();

        GUIElement element = layer.HitTest(new Vector3(position.x, position.y, 0));

        guiTexture.pixelInset = firstRect;

        return element;
    }

    protected GUIElement GuiRaycast(Vector2 position)
    {
        GUILayer layer = Camera.main.GetComponent<GUILayer>();

        GUIElement element = layer.HitTest(new Vector3(position.x, position.y, 0));

        return element;
    }
}

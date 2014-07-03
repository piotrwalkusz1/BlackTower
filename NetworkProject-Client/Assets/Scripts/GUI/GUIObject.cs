using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class GUIObject : MonoBehaviour
{
    protected void OnMouseDown()
    {
        Focus();
    }

    public virtual void OnDropItem(ItemInEquipmentWindow item)
    {
        item.GoToDefaultPlace();
    }

    public virtual void OnDropEquipedItem(ItemInCharacterWindow item)
    {
        item.GoToDefaultPlace();
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

    protected GUIElement GuiRaycast(Vector2 position)
    {
        Rect firstRect = guiTexture.pixelInset;

        guiTexture.pixelInset = new Rect(100000, 100000, guiTexture.pixelInset.width, guiTexture.pixelInset.height);

        GUILayer layer = Camera.main.GetComponent<GUILayer>();

        GUIElement element = layer.HitTest(new Vector3(position.x, position.y, 0));

        guiTexture.pixelInset = firstRect;

        return element;
    }
}

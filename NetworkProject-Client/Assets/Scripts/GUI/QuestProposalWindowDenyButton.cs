using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuestProposalWindowDenyButton : GUIObject
{
    public QuestProposalWindow _window;

    new void OnMouseDown()
    {
        base.OnMouseDown();

        var guiElement = GuiRaycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (guiElement != null && guiElement.GetComponent<GUIObject>() == this)
        {
            _window.Deny();
        }
    }
}
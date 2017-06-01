using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CloseButton : GUIObject
{
    public GameObject _window;

    protected override void OnMouseDown()
    {
        IClosable closable = _window.GetComponent(typeof(IClosable)) as IClosable;

        closable.Close();
    }
}

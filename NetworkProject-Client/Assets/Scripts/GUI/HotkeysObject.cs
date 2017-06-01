using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class HotkeysObject
{
    public abstract Texture2D GetImage();

    public abstract void Use();

    public abstract string GetDescription();

    public abstract bool IsEmpty();
}

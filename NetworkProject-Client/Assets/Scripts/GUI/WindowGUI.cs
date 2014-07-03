using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public delegate bool DoWindowGUIContent(Rect rect);

[System.CLSCompliant(false)]
public class WindowGUI
{
    public int IdWindow { get; set; }
    public DoWindowGUIContent Content { get; set; }
    public string Title { get; set; }
    public Rect WindowRect { get; set; }

    public bool DrawContent()
    {
        return Content(WindowRect);
    }
}
